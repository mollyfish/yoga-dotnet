using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.PhoneViewModels;

namespace Yoga.Models.Services
{
	public class PhoneNumberService : IPhoneNumber
	{
		private YogaDbContext _context;


		public PhoneNumberService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbers()
		{
			return await _context.PhoneNumbers.ToListAsync();
		}

		public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByOwner(int ownerId)
		{
			return await _context.PhoneNumbers.Where(e => e.PersonId == ownerId).ToListAsync();
		}

		public async Task<PhoneViewModel> GetPhoneNumber(int id, int owner)
		{
			var phone = await _context.PhoneNumbers.FindAsync(id);
			var person = _context.People.Where(p => p.Id == owner).First();
			PhoneViewModel phoneData = new PhoneViewModel();
			phoneData.Phone = phone;
			phoneData.Owner = person;
			return phoneData;
		}

		public async Task<PhoneNumber> UpdatePhoneNumber(PhoneNumber phoneNumber)
		{
			_context.PhoneNumbers.Update(phoneNumber);
			await _context.SaveChangesAsync();
			return phoneNumber;
		}

		public async Task<PhoneNumber> CreatePhoneNumber(PhoneNumber phoneNumber)
		{
			await _context.PhoneNumbers.AddAsync(phoneNumber);
			await _context.SaveChangesAsync();
			return phoneNumber;
		}

		public async Task<PhoneNumber> GetMostRecentPhoneNumberByPhone(string phoneNumber)
		{
			var phoneList = await _context.PhoneNumbers.Where(ph => ph.Phone == phoneNumber).OrderBy(ph => ph.DateAdded).ToListAsync();
			PhoneNumber phone = phoneList.Last();
			return phone;
		}
	}
}
