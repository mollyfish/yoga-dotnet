using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EventViewModels.GuestViewModels;

namespace Yoga.Models.Interfaces
{
	public interface IGuest
	{
		/// <summary>
		/// Get list of guests
		/// </summary>
		/// <returns>List of guests</returns>
		Task<IEnumerable<EventGuest>> GetGuests(int Id);

		/// <summary>
		/// Get list of guests
		/// </summary>
		/// <returns>List of guests</returns>
		Task<EventGuest> GetGuestById(int id);

		/// <summary>
		/// Get list of guests for display
		/// </summary>
		/// <returns>List of guests</returns>
		Task<IEnumerable<GuestViewModel>> GetGuestsForDisplay(int Id);

		/// <summary>
		/// Get list of guests for display
		/// </summary>
		/// <returns>List of guests</returns>
		//Task<IEnumerable<GuestViewModel>> GetGuestsForDisplay(string sortOrder, string searchString);

		/// <summary>
		/// Creates a guest
		/// </summary>
		/// <returns>the new guest</returns>
		Task<EventGuest> CreateGuest(EventGuest guest);

		/// <summary>
		/// update a guest
		/// </summary>
		/// <param name="guest"></param>
		/// <returns>the updated guest</returns>
		Task<EventGuest> UpdateGuest(EventGuest guest);

		/// <summary>
		/// delete a guest
		/// </summary>
		/// <param name="guest"></param>
		/// <returns>empty task</returns>
		Task DeleteGuest(int Id);
	}
}
