using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.EmailViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class EmailController : Controller
	{
		private readonly IPeople _people;
		//private readonly IAddress _addresses;
		//private readonly IPhoneNumber _phoneNumbers;
		private readonly IEmailAddress _emailAddresses;

		public EmailController(
			IPeople people,
			//IAddress addresses,
			//IPhoneNumber phoneNumbers,
			IEmailAddress emailAddresses)
		{
			_people = people;
			//_addresses = addresses;
			//_phoneNumbers = phoneNumbers;
			_emailAddresses = emailAddresses;
		}

		[HttpGet]
		public async Task<IActionResult> Create(int Id)
		{
			AddEmailViewModel model = new AddEmailViewModel();
			model.Owner = await _people.GetPerson(Id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newEmail,Owner")] AddEmailViewModel evm)
		{
			if (ModelState.IsValid)
			{
				evm.newEmail.DateAdded = DateTime.Now;
				var emailAddresses = await _emailAddresses.GetEmailAddressesByOwner(evm.Owner.Id);
				if (emailAddresses.Count() == 0)
				{
					evm.newEmail.IsPrimary = true;
				}
				await _emailAddresses.CreateEmailAddress(evm.newEmail);
				return RedirectToAction("Details", "People", new { id = evm.Owner.Id });
			}
			return View(evm);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var email = await _emailAddresses.GetEmailAddress(id);
			return View(email);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Owner")] EmailViewModel evm)
		{
			//if (ModelState.IsValid)
			//{
			//	var emailAddresses = await _emailAddresses.GetEmailAddressesByOwner(evm.Owner.Id);
			//	if (evm.Email.IsPrimary)
			//	{
			//		foreach (var email in emailAddresses)
			//		{
			//			if (email.IsPrimary && email.Id != evm.Email.Id)
			//			{
			//				email.IsPrimary = false;
			//				await _emailAddresses.UpdateEmailAddress(email);
			//			}
			//		}
			//	} else
			//	{
			//		if (emailAddresses.Count() == 1 && emailAddresses.First().Id == evm.Email.Id)
			//		{
			//			evm.Email.IsPrimary = true;
			//		}
			//	}
			//}
			if (ModelState.IsValid)
			{
				await _emailAddresses.UpdateEmailAddress(evm.Email);
			}
			var emailAddresses = await _emailAddresses.GetEmailAddressesByOwner(evm.Owner.Id);
			if (evm.Email.IsPrimary)
			{
				foreach (var email in emailAddresses)
				{
					if (email.IsPrimary && email.Id != evm.Email.Id)
					{
						email.IsPrimary = false;
						await _emailAddresses.UpdateEmailAddress(email);
					}
				}
			}
			else
			{
				if (emailAddresses.Count() == 1 && emailAddresses.First().Id == evm.Email.Id)
				{
					evm.Email.IsPrimary = true;
					await _emailAddresses.UpdateEmailAddress(evm.Email);
				}
			}
			return RedirectToAction("Details", "People", new { id = evm.Owner.Id });

			//return View(evm.Email);
		}

	}
}