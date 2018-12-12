using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.PersonViewModels.DonationViewModels;

namespace Yoga.Models.Services
{
	public class DonationService : IDonation
	{
		private YogaDbContext _context;


		public DonationService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<Donation>> GetDonations()
		{
			return await _context.Donations.ToListAsync();
		}

		public async Task<Donation> GetDonationById(int id)
		{
			return await _context.Donations.FindAsync(id);
		}

		public async Task<DisplayDonationsViewModel> GetDonationsForDisplay()
		{
			var donations = await GetDonations();
			List<Donation> donationList = new List<Donation>();
			DisplayDonationsViewModel model = new DisplayDonationsViewModel();
			model.DonationList = donationList;
			foreach (var donation in donations)
			{
				model.DonationList.Add(donation);
			}
			return model;
		}

	}
}
