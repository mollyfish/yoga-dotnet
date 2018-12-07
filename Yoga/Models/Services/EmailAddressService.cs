using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EmailViewModels;

namespace Yoga.Models.Services
{
	public class EmailAddressService : IEmailAddress
	{
		private YogaDbContext _context;


		public EmailAddressService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<EmailAddress>> GetEmailAddresses()
		{
			return await _context.EmailAddresses.ToListAsync();
		}

		public async Task<IEnumerable<EmailAddress>> GetEmailAddressesByOwner(int ownerId)
		{
			return await _context.EmailAddresses.Where(e => e.PersonId == ownerId).ToListAsync();
		}



		public async Task<EmailViewModel> GetEmailAddress(int Id)
		{
			var email = await _context.EmailAddresses.FindAsync(Id);
			var owner = _context.People.Where(p => p.EmailAddresses.Contains(email)).First();
			EmailViewModel emailData = new EmailViewModel();
			emailData.Email = email;
			emailData.Owner = owner;
			return emailData;
		}

		public async Task<EmailAddress> UpdateEmailAddress(EmailAddress emailAddress)
		{
			_context.EmailAddresses.Update(emailAddress);
			await _context.SaveChangesAsync();
			return emailAddress;
		}

		public async Task<EmailAddress> CreateEmailAddress(EmailAddress emailAddress)
		{
			await _context.EmailAddresses.AddAsync(emailAddress);
			await _context.SaveChangesAsync();
			return emailAddress;
		}

		public async Task<EmailAddress> GetEmailAddressByValue(string value)
		{
			var addressList = await _context.EmailAddresses.Where(e => e.Email == value).ToListAsync();
			EmailAddress emailAddress = addressList.First();
			return emailAddress;
		}
	}
}
