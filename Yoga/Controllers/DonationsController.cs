using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.PersonViewModels.DonationViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class DonationsController : Controller
	{
		private readonly IPeople _people;
		private readonly IAddress _addresses;
		private readonly IPhoneNumber _phoneNumbers;
		private readonly IEmailAddress _emailAddresses;
		private readonly IDonation _donations;

		public DonationsController(
			IPeople people,
			IAddress addresses,
			IPhoneNumber phoneNumbers,
			IEmailAddress emailAddresses,
			IDonation donations)
		{
			_people = people;
			_addresses = addresses;
			_phoneNumbers = phoneNumbers;
			_emailAddresses = emailAddresses;
			_donations = donations;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string sortOrder)
		{
			ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
			var donations = await _donations.GetDonationsForDisplay(sortOrder);

			
			return View(donations);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int Id, int donor)
		{
			DonationViewModel model = new DonationViewModel();
			var donation = await _donations.GetDonationById(Id);
			var person = await _people.GetPerson(donor);
			model.Donation = donation;
			model.Donor = person;
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Create(int? Id)
		{
			AddDonationViewModel model = new AddDonationViewModel();
			model.newDonation = new Donation();
			model.newDonation.Date = DateTime.Today;
			model.Donor = await _people.GetPerson(Id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AddDonationViewModel model)
		{
			if (ModelState.IsValid)
			{
				Person donor = new Person();
				var extantDonors = await _people.GetPersonByName(model.Donor.FirstName, model.Donor.LastName);
				if (extantDonors.Count() == 1)
				{
				Donation donation = new Donation();
				donation.Amount = model.newDonation.Amount;
				donation.Date = model.newDonation.Date;
				donation.DisplayAsAnonymous = model.newDonation.DisplayAsAnonymous;
				donation.DonationType = model.newDonation.DonationType;
				donation.DonorDisplayName = model.newDonation.DonorDisplayName;
				donation.DonorId = extantDonors.First().Id;
				donation.EventId = model.newDonation.EventId;
				donation.Honoree = model.newDonation.Honoree;
				donation.TaxReceiptSent = model.newDonation.TaxReceiptSent;
				donation.TaxReceiptSentDate = model.newDonation.TaxReceiptSentDate;
				donation.ThankYouSent = model.newDonation.ThankYouSent;
				donation.ThankYouSentDate = model.newDonation.ThankYouSentDate;
				await _donations.CreateDonation(donation);
				} else
				{
					model.ErrorMsg = "No donor with that name was found.  Have you added this person to the database? Did you spell their name correctly?";
					return View(model);
				}
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int Id, int donor)
		{
			DonationViewModel model = new DonationViewModel();
			var donation = await _donations.GetDonationById(Id);
			var person = await _people.GetPerson(donor);
			model.Donation = donation;
			model.Donor = person;
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DonationViewModel model)
		{
			if (ModelState.IsValid)
			{
					Donation donation = new Donation();
					donation.Amount = model.Donation.Amount;
					donation.Date = model.Donation.Date;
					donation.DisplayAsAnonymous = model.Donation.DisplayAsAnonymous;
					donation.DonationType = model.Donation.DonationType;
					donation.DonorDisplayName = model.Donation.DonorDisplayName;
					donation.DonorId = model.Donor.Id;
					donation.EventId = model.Donation.EventId;
					donation.Honoree = model.Donation.Honoree;
					donation.TaxReceiptSent = model.Donation.TaxReceiptSent;
					donation.TaxReceiptSentDate = model.Donation.TaxReceiptSentDate;
					donation.ThankYouSent = model.Donation.ThankYouSent;
					donation.ThankYouSentDate = model.Donation.ThankYouSentDate;
					donation.Id = model.Donation.Id;
					await _donations.UpdateDonation(donation);
				
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			Donation donation = await _donations.GetDonationById(Id);
			if (donation == null)
			{
				return NotFound();
			}
			DonationViewModel dvm = new DonationViewModel();
			dvm.Donation = donation;
			var donor = await _people.GetPerson(donation.DonorId);
			dvm.Donor = donor;
			return View(dvm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int Id)
		{
			await _donations.DeleteDonation(Id);
			return RedirectToAction(nameof(Index));
		}
	}
}
