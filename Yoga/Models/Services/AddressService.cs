using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.AddressViewModels;

namespace Yoga.Models.Services
{
	public class AddressService : IAddress
	{
		private YogaDbContext _context;


		public AddressService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<PhysicalAddress>> GetAddresses()
		{
			return await _context.PhysicalAddresses.ToListAsync();
		}

		public async Task<IEnumerable<PhysicalAddress>> GetAddressesByOwner(int ownerId)
		{
			return await _context.PhysicalAddresses.Where(e => e.PersonId == ownerId).ToListAsync();
		}

		public async Task<AddressViewModel> GetPhysicalAddress(int id, int owner)
		{
			var address = await _context.PhysicalAddresses.FindAsync(id);
			var person = _context.People.Where(p => p.Id == owner).First();
			AddressViewModel addressData = new AddressViewModel();
			addressData.Address = address;
			addressData.Owner = person;
			return addressData;
		}

		public async Task<PhysicalAddress> UpdatePhysicalAddress(PhysicalAddress address)
		{
			_context.PhysicalAddresses.Update(address);
			await _context.SaveChangesAsync();
			return address;
		}

		public async Task<PhysicalAddress> CreatePhysicalAddress(PhysicalAddress address)
		{
			await _context.PhysicalAddresses.AddAsync(address);
			await _context.SaveChangesAsync();
			return address;
		}

		public async Task<PhysicalAddress> GetMostRecentPhysicalAddressByTitle(string title)
		{
			var addressList = await _context.PhysicalAddresses.Where(a => a.Title == title).OrderBy(a => a.DateAdded).ToListAsync();
			PhysicalAddress address = addressList.Last();
			return address;
		}
	}
}
