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

		public AddressController(
			IPeople people,
			IAddress addresses
			)
		{
			_people = people;
			_addresses = addresses;
		}

		[HttpGet]
		public async Task<IActionResult> Create(int Id)
		{
			AddAddressViewModel model = new AddAddressViewModel();
			model.Owner = await _people.GetPerson(Id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newAddress,Owner")] AddAddressViewModel avm)
		{
			if (ModelState.IsValid)
			{
				avm.newAddress.DateAdded = DateTime.Now;
				var addresses = await _addresses.GetAddressesByOwner(avm.Owner.Id);
				if (addresses.Count() == 0)
				{
					avm.newAddress.IsPrimary = true;
				}
				await _addresses.CreatePhysicalAddress(avm.newAddress);
				return RedirectToAction("Details", "People", new { id = avm.Owner.Id });
			}
			return View(avm);
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

				var mailAddresses = await _addresses.GetAddressesByOwner(avm.Owner.Id);
				if (avm.Address.IsPrimary)
				{
					foreach (var address in mailAddresses)
					{
						if (address.IsPrimary && address.Id != avm.Address.Id)
						{
							address.IsPrimary = false;
							await _addresses.UpdatePhysicalAddress(address);
						}
					}
				}
				else
				{
					if (mailAddresses.Count() == 1 && mailAddresses.First().Id == avm.Address.Id)
					{
						avm.Address.IsPrimary = true;
						await _addresses.UpdatePhysicalAddress(avm.Address);
					}
				}
				return RedirectToAction("Details", "People", new { id = avm.Owner.Id });
			}
			return View(avm);
		}

	}
}