using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.AddressViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class AddressController : Controller
	{
		private readonly IPeople _people;
		private readonly IAddress _addresses;
		//private readonly IPhoneNumber _phoneNumbers;
		//private readonly IEmailAddress _emailAddresses;

		public AddressController(
			IPeople people,
			//IEmailAddress emailAddresses,
			//IPhoneNumber phoneNumbers,
			IAddress addresses
			)
		{
			_people = people;
			_addresses = addresses;
			//_phoneNumbers = phoneNumbers;
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
			var address = await _addresses.GetPhysicalAddress(id, owner);
			return View(address);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Owner")] AddressViewModel avm)
		{
			if (ModelState.IsValid)
			{
				await _addresses.UpdatePhysicalAddress(avm.Address);
				return RedirectToAction("Details", "People", new { id = avm.Owner.Id });
			}
			return View(avm.Address);
		}

	}
}