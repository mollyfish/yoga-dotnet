using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.PersonViewModels.DonationViewModels;

namespace Yoga.Models.Interfaces
{
	public interface IDonation
	{
		/// <summary>
		/// Get list of donations
		/// </summary>
		/// <returns>List of donations</returns>
		Task<IEnumerable<Donation>> GetDonations();

		/// <summary>
		/// Get list of donations
		/// </summary>
		/// <returns>List of donations</returns>
		Task<Donation> GetDonationById(int id);

		/// <summary>
		/// Get list of donations for display
		/// </summary>
		/// <returns>List of donations</returns>
		Task<IEnumerable<DonationViewModel>> GetDonationsForDisplay(string sortOrder);

		/// <summary>
		/// Creates a donation
		/// </summary>
		/// <returns>the new donation</returns>
		Task<Donation> CreateDonation(Donation donation);

		/// <summary>
		/// update a donation
		/// </summary>
		/// <param name="donation"></param>
		/// <returns>the updated donation</returns>
		Task<Donation> UpdateDonation(Donation donation);
	}
}
