using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yoga.Models;
using Yoga.Models.EventViewModels;
using Yoga.Models.Interfaces;
using Yoga.Models.EventViewModels.TableViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class EventsController : Controller
	{
		private readonly IPeople _people;
		private readonly IEvent _events;
		private readonly ITable _tables;
		private readonly IAddress _addresses;

		public EventsController(
			IPeople people,
			IEvent events,
			ITable tables,
			IAddress addresses)
		{
			_people = people;
			_events = events;
			_tables = tables;
			_addresses = addresses;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			DisplayEventsDataViewModel model = await packEventData();
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int Id)
		{
			DisplayEventDataViewModel model = await packSingleEventData(Id);
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			AddEventViewModel model = new AddEventViewModel();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newEvent,Owner")] AddEventViewModel evm)
		{
			if (ModelState.IsValid)
			{
				evm.newEvent.DateAdded = DateTime.Now;
				
				await _events.CreateEvent(evm.newEvent);
				return RedirectToAction("Details", "Event", new { id = evm.newEvent.Id });
			}
			return View(evm);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			DisplayEventDataViewModel yogaEvent = await packSingleEventData(id);

			return View(yogaEvent);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Event,Owner")] EventViewModel evm)
		{
			if (ModelState.IsValid)
			{
				await _events.UpdateEvent(evm.Event);

				
				return RedirectToAction("Details", "Events", new { id = evm.Event.Id });
			}
			return View(evm);
		}

		private async Task<DisplayEventsDataViewModel> packEventData()
		{
			DisplayEventsDataViewModel model = new DisplayEventsDataViewModel();
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
				EventViewModel evm = new EventViewModel();
				occasion.Tables = tables;
				evm.Event = occasion;
				model.EventList.Add(evm);
			}
			return model;
		}
		private async Task<DisplayEventDataViewModel> packSingleEventData(int id)
		{
			DisplayEventDataViewModel model = new DisplayEventDataViewModel();
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
			model.Event.HostId = yogaEvent.Event.HostId;
			model.Event.LocationId = yogaEvent.Event.LocationId;
			model.Event.Tables = yogaEvent.Event.Tables;
			model.Event.Title = yogaEvent.Event.Title;
			model.Event.Id = yogaEvent.Event.Id;
			return model;
		}
	}
}