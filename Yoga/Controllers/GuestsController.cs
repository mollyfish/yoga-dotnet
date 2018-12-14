using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yoga.Models;
using Yoga.Models.EventViewModels;
using Yoga.Models.Interfaces;
using Yoga.Models.EventViewModels.GuestViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class GuestsController : Controller
	{
		private readonly IPeople _people;
		private readonly IEvent _events;
		private readonly ITable _tables;
		private readonly IAddress _addresses;
		private readonly IGuest _guests;

		public GuestsController(
			IPeople people,
			IEvent events,
			ITable tables,
			IAddress addresses,
			IGuest guests)
		{
			_people = people;
			_events = events;
			_tables = tables;
			_addresses = addresses;
			_guests = guests;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int Id)
		{
			DisplayEventGuestsViewModel egvm = new DisplayEventGuestsViewModel();
			var yogaEvent = await _events.GetEvent(Id);
			egvm.Evm = yogaEvent;
			var guests = await _guests.GetGuestsForDisplay(Id);
			egvm.Guests = guests;
			return View(egvm);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int Id)
		{
			DisplayEventViewModel model = await packSingleEventData(Id);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Create(int Id)
		{
			AddGuestViewModel model = new AddGuestViewModel();
			var result = await _events.GetEvent(Id);
			Event ev = new Event();
			model.Event = result.Event;
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newGuest,FirstName,LastName")] AddGuestViewModel gvm)
		{
			if (ModelState.IsValid)
			{
				var peopleWithGivenName = await _people.GetPersonByName(gvm.FirstName, gvm.LastName);
				List<Person> list = new List<Person>();
				foreach (var person in peopleWithGivenName)
				{
					list.Add(person);
				}
				if (list.Count == 1)
				{
					Person[] people = list.ToArray();
					gvm.newGuest.PersonId = people[0].Id;
				}

				gvm.newGuest.DateAdded = DateTime.Now;

				await _guests.CreateGuest(gvm.newGuest);

				var ev = await _events.GetEvent(gvm.newGuest.EventId);
				ev.Event.Guests.Add(gvm.newGuest);

				return RedirectToAction("Details", "Events", new { id = gvm.newGuest.EventId });
			}
			return View(gvm);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			DisplayEventViewModel yogaEvent = await packSingleEventData(id);

			return View(yogaEvent);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Event,Owner")] EventViewModel gvm)
		{
			if (ModelState.IsValid)
			{
				await _events.UpdateEvent(gvm.Event);


				return RedirectToAction("Details", "Events", new { id = gvm.Event.Id });
			}
			return View(gvm);
		}

		private async Task<DisplayEventsViewModel> packEventData()
		{
			DisplayEventsViewModel model = new DisplayEventsViewModel();
			model.EventList = new List<EventViewModel>();

			var allEvents = await _events.GetEvents();
			var allTables = await _tables.GetTables();
			foreach (var occasion in allEvents)
			{
				List<Table> tables = new List<Table>();
				foreach (var table in allTables)
				{
					if (table.EventId == occasion.Id)
					{

						tables.Add(table);
					}
				}
				foreach (var table in tables)
				{

				}
				EventViewModel gvm = new EventViewModel();
				occasion.Tables = tables;
				gvm.Event = occasion;
				model.EventList.Add(gvm);
			}
			return model;
		}
		private async Task<DisplayEventViewModel> packSingleEventData(int id)
		{
			DisplayEventViewModel model = new DisplayEventViewModel();
			Event resultEvent = new Event();
			model.Event = resultEvent;
			var yogaEvent = await _events.GetEvent(id);
			try
			{

				model.Event.Date = yogaEvent.Event.Date;
			}
			catch (Exception)
			{

				throw;
			}
			model.Event.Guests = yogaEvent.Event.Guests;
			model.Event.LocationId = yogaEvent.Event.LocationId;
			model.Event.Tables = yogaEvent.Event.Tables;
			model.Event.Title = yogaEvent.Event.Title;
			model.Event.Id = yogaEvent.Event.Id;
			return model;
		}
	}
}