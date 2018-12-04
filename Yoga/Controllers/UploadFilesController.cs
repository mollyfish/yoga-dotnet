using Yoga.Data;
using Yoga.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Controllers
{
	public class UploadFilesController : Controller
	{
		private readonly YogaDbContext _context;
		private static List<Donation> newDonations;

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
				// insert the nes donations into the database
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
				List<int> EventIds = new List<int>();
				List<decimal> Amounts = new List<decimal>();
				List<DateTime> Dates = new List<DateTime>();
				List<string> Honorees = new List<string>();
				List<bool> TaxReceiptSent = new List<bool>();
				List<bool> ThankYouSent = new List<bool>();
				List<DateTime> TaxReceiptSentDate = new List<DateTime>();
				List<DateTime> ThankYouSentDate = new List<DateTime>();
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
					EventIds.Add(int.Parse(values[3]));
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
					TaxReceiptSentDate.Add(DateTime.Parse(values[9]));
					ThankYouSentDate.Add(DateTime.Parse(values[10]));
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
						newDonation.DonorId = DonorIds.First().ToString();
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

		private static void UploadUsers(string pathToFile)
		{
			using (var reader = new StreamReader(pathToFile))
			{
				// create lists to hold column info
				List<string> headings = new List<string>();
				List<int> DonorIds = new List<int>();
				List<string> DonorDisplayNames = new List<string>();
				List<bool> DisplayAsAnonymous = new List<bool>();
				List<int> EventIds = new List<int>();
				List<decimal> Amounts = new List<decimal>();
				List<DateTime> Dates = new List<DateTime>();
				List<string> Honorees = new List<string>();
				List<bool> TaxReceiptSent = new List<bool>();
				List<bool> ThankYouSent = new List<bool>();
				List<DateTime> TaxReceiptSentDate = new List<DateTime>();
				List<DateTime> ThankYouSentDate = new List<DateTime>();
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
					EventIds.Add(int.Parse(values[3]));
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
					TaxReceiptSentDate.Add(DateTime.Parse(values[9]));
					ThankYouSentDate.Add(DateTime.Parse(values[10]));
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
						newDonation.DonorId = DonorIds.First().ToString();
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
	}
}