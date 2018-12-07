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
		private readonly IEmailAddress _emailAddresses;

		public PeopleController(
			IPeople people, 
			IAddress addresses,
			IPhoneNumber phoneNumbers,
			IEmailAddress emailAddresses)
		{
			_people = people;
			_addresses = addresses;
			_phoneNumbers = phoneNumbers;
			_emailAddresses = emailAddresses;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			DisplayPeopleDataViewModel model = await packPeopleData();
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

				EmailAddress email = new EmailAddress();
				email.Email = model.Email;
				email.PersonId = newPerson.Id;
				email.DateAdded = DateTime.Now;
				await _emailAddresses.CreateEmailAddress(email);

				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int Id)
		{
			DisplayPersonDataViewModel model = await packPersonData(Id);
			return View(model);
		}

	

		[HttpGet]
		public async Task<IActionResult> EditName(int id)
		{
			var person = await _people.GetPerson(id);
			return View(person);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, [Bind("Id,FirstName,LastName")] Person person)
		{
			if (ModelState.IsValid)
			{
				await _people.UpdatePerson(person);
				// make Person Updates
				return RedirectToAction("Details", new { id = person.Id });
			}
				return View(person);
		}

		private async Task<DisplayPeopleDataViewModel> packPeopleData()
		{
			DisplayPeopleDataViewModel model = new DisplayPeopleDataViewModel();
			model.PeopleList = new List<Person>();
			var people = await _people.GetPeople();
			var addresses = await _addresses.GetAddresses();
			var phones = await _phoneNumbers.GetPhoneNumbers();
			var emails = await _emailAddresses.GetEmailAddresses();
			foreach (var person in people)
			{
				List<PhysicalAddress> mailingAddresses = new List<PhysicalAddress>();
				List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
				List<EmailAddress> emailAddresses = new List<EmailAddress>();
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
				foreach (var item in emails)
				{
					if (item.PersonId == person.Id)
					{
						emailAddresses.Add(item);
					}
				}
				person.EmailAddresses = emailAddresses;
				model.PeopleList.Add(person);
			}
			return model;
		}

		private async Task<DisplayPersonDataViewModel> packPersonData(int Id)
		{
			DisplayPersonDataViewModel model = new DisplayPersonDataViewModel();
			model.person = new Person();
			var person = await _people.GetPerson(Id);
			var addresses = await _addresses.GetAddresses();
			var phones = await _phoneNumbers.GetPhoneNumbers();
			var emails = await _emailAddresses.GetEmailAddresses();
			
				List<PhysicalAddress> mailingAddresses = new List<PhysicalAddress>();
				List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
				List<EmailAddress> emailAddresses = new List<EmailAddress>();
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
				foreach (var item in emails)
				{
					if (item.PersonId == person.Id)
					{
						emailAddresses.Add(item);
					}
				}
				person.EmailAddresses = emailAddresses;
				model.person = person;
			
			return model;
		}
	}
}
