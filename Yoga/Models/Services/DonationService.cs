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

		/// <summary>
		/// Get list of donations with no special handling
		/// </summary>
		/// <returns>list of donations</returns>
		public async Task<IEnumerable<Donation>> GetDonations()
		{
			return await _context.Donations.ToListAsync();
		}

		/// <summary>
		/// Get single donation by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>single donation</returns>
		public async Task<Donation> GetDonationById(int id)
		{
			return await _context.Donations.FindAsync(id);
		}

		/// <summary>
		/// Gets all donations with related donor info
		/// </summary>
		/// <returns>list of DonationVeiwModels that contain Person info and Donation info</returns>
		public async Task<IEnumerable<DonationViewModel>> GetDonationsForDisplay(string sortOrder)
		{
			var donations = await GetDonations();
			List<Donation> donationList = new List<Donation>();
			List<Person> donorList = new List<Person>();



			List<DonationViewModel> unsortedList = new List<DonationViewModel>();
			foreach (var item in donations)
			{
				DonationViewModel vm = new DonationViewModel();
				vm.Donation = item;
				vm.Donor = await _context.People.FirstOrDefaultAsync(p => p.Id == item.DonorId);
				unsortedList.Add(vm);
			}
			List<DonationViewModel> model = new List<DonationViewModel>();
			switch (sortOrder)
			{
				case "name_desc":
					model = unsortedList.OrderByDescending(m => m.Donor.LastName).ThenByDescending(m => m.Donor.FirstName).ToList();
					break;
				case "Date":
					model = unsortedList.OrderBy(m => m.Donation.Date).ThenBy(m => m.Donor.LastName).ThenBy(m => m.Donor.FirstName).ToList();
					break;
				case "date_desc":
					model = unsortedList.OrderByDescending(m => m.Donation.Date).ThenByDescending(m => m.Donor.LastName).ThenByDescending(m => m.Donor.FirstName).ToList();
					break;
				default:
					model = unsortedList.OrderBy(m => m.Donor.LastName).ThenBy(m => m.Donor.FirstName).ToList();
					break;
			}
			
			return model;
		}

		/// <summary>
		/// Creates a new donation in the database
		/// </summary>
		/// <param name="donation"></param>
		/// <returns>the new donation</returns>
		public async Task<Donation> CreateDonation(Donation donation)
		{
			await _context.Donations.AddAsync(donation);
			await _context.SaveChangesAsync();
			return donation;
		}

		/// <summary>
		/// update a donation
		/// </summary>
		/// <param name="address"></param>
		/// <returns>the updated donation</returns>
		public async Task<Donation> UpdateDonation(Donation donation)
		{
			_context.Donations.Update(donation);
			await _context.SaveChangesAsync();
			return donation;
		}

		/// <summary>
		/// delete a donation
		/// </summary>
		/// <param name="address"></param>
		/// <returns>empty task</returns>
		public async Task DeleteDonation(int Id)
		{
			Donation donation = await GetDonationById(Id);
			_context.Donations.Remove(donation);
			await _context.SaveChangesAsync();
		}

	}
}
