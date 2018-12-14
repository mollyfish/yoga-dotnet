using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EventViewModels;

namespace Yoga.Models.Services
{
	public class EventService : IEvent
	{
		private YogaDbContext _context;


		public EventService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<Event>> GetEvents()
		{
			return await _context.Events.ToListAsync();
		}

		public async Task<IEnumerable<Event>> GetEventsByTitle(string title)
		{
			return await _context.Events.Where(e => e.Title == title).ToListAsync();
		}

		public async Task<EventViewModel> GetEvent(int id)
		{
			var yogaEvent = await _context.Events.FindAsync(id);
			EventViewModel yogaEventData = new EventViewModel();
			yogaEventData.Event = yogaEvent;
			return yogaEventData;
		}

		public async Task<DisplayEventViewModel> GetEventDetailsForDisplay(int Id)
		{
			DisplayEventViewModel model = new DisplayEventViewModel();
			var yogaEvent = await _context.Events.FindAsync(Id);
			var address = await _context.PhysicalAddresses.FindAsync(yogaEvent.LocationId);
			var hostIdList = await _context.EventHosts.Where(eh => eh.EventId == Id).ToListAsync();
			var guestIdList = await _context.EventGuests.Where(eg => eg.EventId == Id).ToListAsync();
			foreach (var guest in guestIdList)
			{
				var person = await _context.People.FindAsync(guest.PersonId);
				guest.GuestName = $"{person.FirstName} {person.LastName}";
			}
			var tableIdList = await _context.EventTables.Where(et => et.EventId == Id).ToListAsync();
			List<TableCaptain> captainIdList = new List<TableCaptain>();
			List<Table> tables = new List<Table>();
			foreach (var table in tableIdList)
			{
				var cap = await _context.TableCaptains.FirstOrDefaultAsync(tc => tc.TableId == table.TableId);
				captainIdList.Add(cap);
				var actualTable = await _context.Tables.FirstOrDefaultAsync(ac => ac.Id == table.TableId);
				tables.Add(actualTable);
			}
			foreach (var table in tables)
			{
				List<TableGuest> tableGuestIdList = await _context.TableGuests.Where(tg => tg.TableId == table.Id).ToListAsync();
				foreach (var tableGuest in tableGuestIdList)
				{
					var actualPerson = await _context.People.FirstOrDefaultAsync(ap => ap.Id == tableGuest.PersonId);
					tableGuest.GuestName = $"{actualPerson.FirstName} {actualPerson.LastName}";
					table.Guests.Add(tableGuest);
				}
			}
			model.Event = yogaEvent;
			model.EventGuests = guestIdList;
			model.Tables = tables;
			model.Location = address;
			return model;
		}

		public async Task<Event> UpdateEvent(Event yogaEvent)
		{
			_context.Events.Update(yogaEvent);
			await _context.SaveChangesAsync();
			return yogaEvent;
		}

		public async Task<Event> CreateEvent(Event yogaEvent)
		{
			await _context.Events.AddAsync(yogaEvent);
			await _context.SaveChangesAsync();
			return yogaEvent;
		}

		public async Task<Event> GetMostRecentEventByTitle(string title)
		{
			var yogaEventList = await _context.Events.Where(a => a.Title == title).OrderBy(a => a.DateAdded).ToListAsync();
			Event yogaEvent = yogaEventList.Last();
			return yogaEvent;
		}
	}
}
