using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class UploadFilesController : Controller
	{
		private readonly YogaDbContext _context;
		private static List<Donation> newDonations;
		private static List<Person> newPeople;
		private static List<PhysicalAddress> newAddresses;
		private static List<PhoneNumber> newPhoneNumbers;
		private static List<EmailAddress> newEmailAddresses;

		public UploadFilesController(YogaDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Uploads a file
		/// </summary>
		/// <param name="file">the file to upload</param>
		/// <param name="dataType">the type of data in the file being uploaded: donation, user, event or class</param>
		/// <returns></returns>
		[HttpPost("UploadFiles")]
		public async Task<IActionResult> Post(IFormFile file, string dataType)
		{

			// is the file a CSV?
			string fileName = file.FileName;
			int length = fileName.Length;
			// if not CSV, abort upload
			if (fileName[length - 4] != '.' || fileName[length - 3] != 'c' || fileName[length - 2] != 's' || fileName[length - 1] != 'v')
			{
				return View("FailedUpload");
			}

			// get full path to file in temp location
			var filePath = Path.GetTempFileName();

			// in the file is not empty, start reading
			if (file.Length > 0)
			{
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
			}

			// if the file contains DONATION data...
			if (dataType == "donation")
			{
				// create a list to hold the new data
				newDonations = new List<Donation>();
				// parse the Donation data
				UploadDonations(filePath);
				// insert the new donations into the database
				while (newDonations.Count > 0)
				{
					// check for a doantion on the same date, of the same amount, 
					// by the same person, of hte same donation type - this is a hacky check for duplicates
					var alreadyExists = _context.Donations.FirstOrDefault(d => d.Date == newDonations.First().Date && d.DonorId == newDonations.First().DonorId && d.Amount == newDonations.First().Amount && d.DonationType == newDonations.First().DonationType && d.DonorDisplayName == newDonations.First().DonorDisplayName);
					// if the donation does not exist already, add it to the database
					if (alreadyExists == null)
					{
						await _context.Donations.AddAsync(newDonations.First());
						await _context.SaveChangesAsync();
					}
					// remove the added donation from the list
					newDonations.RemoveAt(0);
				}
			} else if (dataType == "person")
			{
				// create a list to hold the new data
				newPeople = new List<Person>();
				newAddresses = new List<PhysicalAddress>();
				newEmailAddresses = new List<EmailAddress>();
				newPhoneNumbers = new List<PhoneNumber>();
				// parse the Person data
				UploadPeople(filePath);
				// insert the new people into the database
				while (newPeople.Count > 0)
				{
					// check for a person with the same name and email - if found, the new person is a duplicate
					var alreadyExists = false;
					var possibleDupe = await _context.People.AnyAsync(p => p.FirstName == newPeople.First().FirstName && p.LastName == newPeople.First().LastName);
					if (possibleDupe)
					{
						var extant = await _context.People.Where(p => p.FirstName == newPeople.First().FirstName && p.LastName == newPeople.First().LastName).ToListAsync();
						foreach (var person in extant)
						{
							var emails = await _context.EmailAddresses.Where(e => e.PersonId == person.Id).ToListAsync();
							foreach (var email in emails)
							{
							if (email.Email == newEmailAddresses.First().Email)
								{
									alreadyExists = true;
									break;
								}
							}
						}
					}
					
					// if the person does not exist already, add it to the database
					if (alreadyExists == false)
					{
						await _context.People.AddAsync(newPeople.First());
						await _context.SaveChangesAsync();
						int newId = newPeople.First().Id;
						//var peopleList = await _context.People.OrderBy(a => a.DateAdded).ToListAsync();

						newAddresses.First().PersonId = newId;
						await _context.PhysicalAddresses.AddAsync(newAddresses.First());
						await _context.SaveChangesAsync();

						newEmailAddresses.First().PersonId = newId;
						await _context.EmailAddresses.AddAsync(newEmailAddresses.First());
						await _context.SaveChangesAsync();

						newPhoneNumbers.First().PersonId = newId;
						await _context.PhoneNumbers.AddAsync(newPhoneNumbers.First());
						await _context.SaveChangesAsync();
					}
					// remove the added objects from the appropriate lists
					newPeople.RemoveAt(0);
					newAddresses.RemoveAt(0);
					newEmailAddresses.RemoveAt(0);
					newPhoneNumbers.RemoveAt(0);
				}
			}
			// return the user to the view
			return View(nameof(Index));

		}

		private static void UploadDonations(string pathToFile)
		{
			using (var reader = new StreamReader(pathToFile))
			{
				// create lists to hold column info
				List<string> headings = new List<string>();
				List<int> DonorIds = new List<int>();
				List<string> DonorDisplayNames = new List<string>();
				List<bool> DisplayAsAnonymous = new List<bool>();
				List<int?> EventIds = new List<int?>();
				List<decimal> Amounts = new List<decimal>();
				List<DateTime> Dates = new List<DateTime>();
				List<string> Honorees = new List<string>();
				List<bool> TaxReceiptSent = new List<bool>();
				List<bool> ThankYouSent = new List<bool>();
				List<DateTime?> TaxReceiptSentDate = new List<DateTime?>();
				List<DateTime?> ThankYouSentDate = new List<DateTime?>();
				List<string> HowPaid = new List<string>();

				// while there is still content to read...
				int counter = 0;
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					// strip the column headings
					if (counter == 0)
					{
						for (int i = 0; i < values.Length; i++)
						{
							headings.Add(values[i]);
						}
						counter++;
						continue;
					}
					// pull the appropriate values and add them to the list that represents that column
					DonorIds.Add(int.Parse(values[0]));
					DonorDisplayNames.Add(values[1]);
					if (values[2] == "yes")
					{
						DisplayAsAnonymous.Add(true);
					}
					else
					{
						DisplayAsAnonymous.Add(false);
					}
					if (values[3] != "")
					{
						EventIds.Add(int.Parse(values[3]));
					} else
					{
						EventIds.Add(null);
					}
					Amounts.Add(decimal.Parse(values[4]));
					Dates.Add(DateTime.Parse(values[5]));
					Honorees.Add(values[6]);
					if (values[7] == "yes")
					{
						TaxReceiptSent.Add(true);
					}
					else
					{
						TaxReceiptSent.Add(false);
					}
					if (values[8] == "yes")
					{
						ThankYouSent.Add(true);
					}
					else
					{
						ThankYouSent.Add(false);
					}
					if (values[9] != "")
					{
						TaxReceiptSentDate.Add(DateTime.Parse(values[9]));
					} else
					{
						TaxReceiptSentDate.Add(null);
					}
					if (values[10] != "")
					{
						ThankYouSentDate.Add(DateTime.Parse(values[10]));
					} else
					{
						ThankYouSentDate.Add(null);
					}
					if (values[11].ToLower() == "cash")
					{
						HowPaid.Add("cash");
					}
					else if (values[11].ToLower() == "check")
					{
						HowPaid.Add("check");
					}
					else
					{
						HowPaid.Add("card");
					}
					counter++;
				}
				// all data is now in lists
				// all donations must have a donation date, so run the while loop off that list
				try
				{
					// while there is still stuff to deal with...
					while (Dates.Count != 0)
					{
						// make a new Donation instance and add data to it
						Donation newDonation = new Donation();
						// add the data
						newDonation.DonorId = DonorIds.First();
						// remove that data from its list, reducing the list by 1 every time this while loop runs
						DonorIds.RemoveAt(0);
						// do that for all data points
						newDonation.DonorDisplayName = DonorDisplayNames.First();
						DonorDisplayNames.RemoveAt(0);
						newDonation.DisplayAsAnonymous = DisplayAsAnonymous.First();
						DisplayAsAnonymous.RemoveAt(0);
						newDonation.EventId = EventIds.First();
						EventIds.RemoveAt(0);
						newDonation.Amount = Amounts.First();
						Amounts.RemoveAt(0);
						newDonation.Date = Dates.First();
						Dates.RemoveAt(0);
						newDonation.Honoree = Honorees.First();
						Honorees.RemoveAt(0);
						newDonation.TaxReceiptSent = TaxReceiptSent.First();
						TaxReceiptSent.RemoveAt(0);
						newDonation.ThankYouSent = ThankYouSent.First();
						ThankYouSent.RemoveAt(0);
						newDonation.TaxReceiptSentDate = TaxReceiptSentDate.First();
						TaxReceiptSentDate.RemoveAt(0);
						newDonation.ThankYouSentDate = ThankYouSentDate.First();
						ThankYouSentDate.RemoveAt(0);
						if (HowPaid.First() == "cash")
						{
							newDonation.DonationType = DonationType.cash;
						}
						else if (HowPaid.First() == "check")
						{
							newDonation.DonationType = DonationType.check;
						}
						else
						{
							newDonation.DonationType = DonationType.card;
						}
						// add the completed donation object to the list of new donations
						newDonations.Add(newDonation);
					}
				}
				catch (Exception e)
				{
					throw;
				}

			}
		}

		private static void UploadPeople(string pathToFile)
		{
			using (var reader = new StreamReader(pathToFile))
			{
				// create lists to hold column info
				List<string> headings = new List<string>();
				List<string> FirstNames = new List<string>();
				List<string> LastNames = new List<string>();
				List<string> EmailAddresses = new List<string>();
				List<string> AddressTitles = new List<string>();
				List<string> StreetAddresses = new List<string>();
				List<string> StreetAddressesCont = new List<string>();
				List<string> Cities = new List<string>();
				List<string> States = new List<string>();
				List<string> ZipCodes = new List<string>();
				List<string> Telephones = new List<string>();
				
				// while there is still content to read...
				int counter = 0;
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					// strip the column headings
					if (counter == 0)
					{
						for (int i = 0; i < values.Length; i++)
						{
							headings.Add(values[i]);
						}
						counter++;
						continue;
					}
					// pull the appropriate values and add them to the list that represents that column
					FirstNames.Add(values[0]);
					LastNames.Add(values[1]);
					EmailAddresses.Add(values[2]);
					AddressTitles.Add(values[3]);
					StreetAddresses.Add(values[4]);
					StreetAddressesCont.Add(values[5]);
					Cities.Add(values[6]);
					States.Add(values[7]);
					ZipCodes.Add(values[8]);
					Telephones.Add(values[9]);
					counter++;
				}
				// all data is now in lists
				// all people must have a last name, so run the while loop off that list
				try
				{
					// while there is still stuff to deal with...
					while (LastNames.Count != 0)
					{
						// make a new Person instance and add data to it
						Person newPerson = new Person();
						newPerson.FullName = $"{FirstNames.First()} {LastNames.First()}";
						// add the data
						newPerson.FirstName = FirstNames.First();
						// remove that data from its list, reducing the list by 1 every time this while loop runs
						FirstNames.RemoveAt(0);
						// do that for all data points
						newPerson.LastName = LastNames.First();
						LastNames.RemoveAt(0);
						newPerson.DateAdded = DateTime.Now;

						// make a new address instance and add data to it
						PhysicalAddress newAddress = new PhysicalAddress();
						// add the data
						newAddress.StreetAddress = StreetAddresses.First();
						// remove that data from its list, reducing the list by 1
						StreetAddresses.RemoveAt(0);
						// do that for all data points
						newAddress.StreetAddressCont = StreetAddressesCont.First();
						StreetAddressesCont.RemoveAt(0);
						newAddress.City = Cities.First();
						Cities.RemoveAt(0);
						newAddress.State = States.First();
						States.RemoveAt(0);
						newAddress.ZipCode = ZipCodes.First();
						ZipCodes.RemoveAt(0);
						newAddress.Title = AddressTitles.First();
						AddressTitles.RemoveAt(0);
						newAddress.DateAdded = DateTime.Now;
						newAddress.IsPrimary = true;

						// do the same for phone numbers
						PhoneNumber newTelephone = new PhoneNumber();
						newTelephone.DateAdded = DateTime.Now;
						newTelephone.Phone = Telephones.First();
						Telephones.RemoveAt(0);
						newTelephone.IsPrimary = true;

						// and email addresses
						EmailAddress newEmail = new EmailAddress();
						newEmail.DateAdded = DateTime.Now;
						newEmail.Email = EmailAddresses.First();
						EmailAddresses.RemoveAt(0);
						newEmail.IsPrimary = true;
						
						
						// add the completed objects to the appropriate list
						newPeople.Add(newPerson);
						newAddresses.Add(newAddress);
						newPhoneNumbers.Add(newTelephone);
						newEmailAddresses.Add(newEmail);
					}
				}
				catch (Exception e)
				{
					throw;
				}

			}
		}
	}
}