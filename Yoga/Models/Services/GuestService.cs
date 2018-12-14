using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EventViewModels.GuestViewModels;

namespace Yoga.Models.Services
{
	public class GuestService : IGuest
	{
		private YogaDbContext _context;


		public GuestService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<EventGuest> CreateGuest(EventGuest guest)
		{
			await _context.EventGuests.AddAsync(guest);
			await _context.SaveChangesAsync();
			return guest;
		}

		public Task DeleteGuest(int Id)
		{
			throw new NotImplementedException();
		}

		public Task<EventGuest> GetGuestById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<EventGuest>> GetGuests(int Id)
		{
			var guests = await _context.EventGuests.Where(g => g.EventId == Id).ToListAsync();
			return guests;
		}

		public async Task<IEnumerable<GuestViewModel>> GetGuestsForDisplay(int Id)
		{
			List<GuestViewModel> list = new List<GuestViewModel>();
			var guests = await _context.EventGuests.Where(g => g.EventId == Id).ToListAsync();
			foreach (var item in guests)
			{
				GuestViewModel gvm = new GuestViewModel();
				gvm.Guest = item;
				gvm.Event = await _context.Events.FindAsync(Id);
				var person = await _context.People.FindAsync(item.PersonId);
				gvm.GuestName = $"{person.FirstName} {person.LastName}";
				list.Add(gvm);
			}
			return list;
		}

	

		public Task<EventGuest> UpdateGuest(EventGuest guest)
		{
			throw new NotImplementedException();
		}
	}
}
