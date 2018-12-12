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
