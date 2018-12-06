using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		public async Task<PhoneNumber> GetPhoneNumber(int Id)
		{
			return await _context.PhoneNumbers.FindAsync(Id);
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
