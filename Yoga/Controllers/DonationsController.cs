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
		public async Task<IActionResult> Index()
		{
			var donations = await _donations.GetDonationsForDisplay();
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
		public async Task<IActionResult> Create(int Id)
		{
			AddDonationViewModel model = new AddDonationViewModel();
			model.Donor = await _people.GetPerson(Id);
			return View(model);
		}
	}
}
