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
		Task<DisplayDonationsViewModel> GetDonationsForDisplay();
	}
}
