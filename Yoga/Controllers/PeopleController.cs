using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.PersonViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class PeopleController : Controller
	{
		private readonly IPeople _people;
		private readonly IAddress _addresses;
		private readonly IPhoneNumber _phoneNumbers;

		public PeopleController(
			IPeople people, 
			IAddress addresses,
			IPhoneNumber phoneNumbers)
		{
			_people = people;
			_addresses = addresses;
			_phoneNumbers = phoneNumbers;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			DisplayPersonDataViewModel model = new DisplayPersonDataViewModel();
			model.PeopleList = new List<Person>();
			var people = await _people.GetPeople();
			var addresses = await _addresses.GetAddresses();
			var phones = await _phoneNumbers.GetPhoneNumbers();
			foreach (var person in people)
			{
				List<PhysicalAddress> mailingAddresses = new List<PhysicalAddress>();
				List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
				foreach (var address in addresses)
				{
					if (address.PersonId == person.Id)
					{
						mailingAddresses.Add(address);
					}
				}
				person.MailingAddresses = mailingAddresses;
				foreach (var phone in phones)
				{
					if (phone.PersonId == person.Id)
					{
						phoneNumbers.Add(phone);
					}
				}
				person.PhoneNumbers = phoneNumbers;
				model.PeopleList.Add(person);
			}
			
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreatePersonViewModel model)
		{
			if (ModelState.IsValid)
			{
				Person person = new Person();
				person.FirstName = model.FirstName;
				person.LastName = model.LastName;
				person.FullName = $"{model.FirstName} {model.LastName}";
				person.DateAdded = DateTime.Now;
				await _people.CreatePerson(person);

				var newPerson = await _people.GetMostRecentPersonByName(person.FullName);

				PhysicalAddress address = new PhysicalAddress();
				address.StreetAddress = model.StreetAddress;
				address.StreetAddressCont = model.StreetAddressCont;
				address.City = model.City;
				address.State = model.State;
				address.ZipCode = model.ZipCode;
				address.PersonId = newPerson.Id;
				address.DateAdded = DateTime.Now;
				await _addresses.CreatePhysicalAddress(address);

				PhoneNumber phone = new PhoneNumber();
				phone.Phone = model.PhoneNumber;
				phone.PersonId = newPerson.Id;
				phone.DateAdded = DateTime.Now;
				await _phoneNumbers.CreatePhoneNumber(phone);

				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
	}
}
