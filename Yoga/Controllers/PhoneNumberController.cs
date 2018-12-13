using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.PersonViewModels.PhoneViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class PhoneNumberController : Controller
	{
		private readonly IPeople _people;
		private readonly IPhoneNumber _phoneNumbers;

		public PhoneNumberController(
			IPeople people,
			IPhoneNumber phoneNumbers)
		{
			_people = people;
			_phoneNumbers = phoneNumbers;
		}

		[HttpGet]
		public async Task<IActionResult> Create(int Id)
		{
			AddPhoneViewModel model = new AddPhoneViewModel();
			model.Owner = await _people.GetPerson(Id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newPhone,Owner")] AddPhoneViewModel phvm)
		{
			if (ModelState.IsValid)
			{
				phvm.newPhone.DateAdded = DateTime.Now;
				var phones = await _phoneNumbers.GetPhoneNumbersByOwner(phvm.Owner.Id);
				if (phones.Count() == 0)
				{
					phvm.newPhone.IsPrimary = true;
				}
				await _phoneNumbers.CreatePhoneNumber(phvm.newPhone);
				return RedirectToAction("Details", "People", new { id = phvm.Owner.Id });
			}
			return View(phvm);
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

				var phoneNumbers = await _phoneNumbers.GetPhoneNumbersByOwner(phvm.Owner.Id);
				if (phvm.Phone.IsPrimary)
				{
					foreach (var number in phoneNumbers)
					{
						if (number.IsPrimary && number.Id != phvm.Phone.Id)
						{
							number.IsPrimary = false;
							await _phoneNumbers.UpdatePhoneNumber(number);
						}
					}
				}
				else
				{
					if (phoneNumbers.Count() == 1 && phoneNumbers.First().Id == phvm.Phone.Id)
					{
						phvm.Phone.IsPrimary = true;
						await _phoneNumbers.UpdatePhoneNumber(phvm.Phone);
					}
				}
				return RedirectToAction("Details", "People", new { id = phvm.Owner.Id });
			}
			return View(phvm);
		}

	}
}