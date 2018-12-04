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
	public class VIPService : IVIP
	{
		private YogaDbContext _context;
		private YogaUsersDbContext _userContext;
		private readonly UserManager<User> _usermanager;


		public VIPService(
			YogaDbContext context,
			YogaUsersDbContext userContext,
			UserManager<User> userManager
		)
		{
			_context = context;
			_userContext = userContext;
			_usermanager = userManager;
		}

		public async Task<IEnumerable<VIP>> GetVIPs()
		{
			return await _context.VIPs.ToListAsync();
		}

		public async Task<VIP> GetVIP(int Id)
		{
			return await _context.VIPs.FindAsync(Id);
		}

		public async Task<VIP> UpdateVIP(VIP vip)
		{
			_context.VIPs.Update(vip);
			var user = await _usermanager.FindByEmailAsync(vip.Email);

			user.Email = vip.Email;
			user.FirstName = vip.FirstName;
			user.LastName = vip.LastName;
			user.FullName = $"{user.FirstName} {user.LastName}";
			await _usermanager.UpdateAsync(user);
			await _context.SaveChangesAsync();
			return vip;
		}

		public async Task<VIP> CreateVIP(VIP vip)
		{
			await _context.VIPs.AddAsync(vip);
			await _context.SaveChangesAsync();
			return vip;
		}

	}
}
