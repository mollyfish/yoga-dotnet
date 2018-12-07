using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.PhoneViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class PhoneNumberController : Controller
	{
		private readonly IPeople _people;
		//private readonly IAddress _addresses;
		private readonly IPhoneNumber _phoneNumbers;
		//private readonly IEmailAddress _emailAddresses;

		public PhoneNumberController(
			IPeople people,
			//IAddress addresses,
			//IEmailAddress emailAddresses,
			IPhoneNumber phoneNumbers)
		{
			_people = people;
			//_addresses = addresses;
			_phoneNumbers = phoneNumbers;
			//_emailAddresses = emailAddresses;
		}

		[HttpGet]
		public async Task<IActionResult> Create(int Id)
		{
			var owner = await _people.GetPerson(Id);
			return View(owner);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id, int owner)
		{
			var phone = await _phoneNumbers.GetPhoneNumber(id, owner);
			return View(phone);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Phone,Owner")] PhoneViewModel phvm)
		{
			if (ModelState.IsValid)
			{
				await _phoneNumbers.UpdatePhoneNumber(phvm.Phone);
				return RedirectToAction("Details", "People", new { id = phvm.Owner.Id });
			}
			return View(phvm.Phone);
		}

	}
}