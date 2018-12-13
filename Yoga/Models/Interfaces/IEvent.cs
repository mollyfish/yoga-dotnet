using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EventViewModels;

namespace Yoga.Models.Interfaces
{
	public interface IEvent
	{
		/// <summary>
		/// Get list of events
		/// </summary>
		/// <returns>List of events</returns>
		Task<IEnumerable<Event>> GetEvents();

		/// <summary>
		/// Get list of events by title
		/// </summary>
		/// <param name="Id">owner Id</param>
		/// <returns>List of events that have the given title</returns>
		Task<IEnumerable<Event>> GetEventsByTitle(string title);

		/// <summary>
		/// Get single event by Id
		/// </summary>
		/// <returns>Single event</returns>
		Task<EventViewModel> GetEvent(int Id);

		/// <summary>
		/// Update single event
		/// </summary>
		/// <returns>the updated event</returns>
		Task<Event> UpdateEvent(Event yogaEvent);

		/// <summary>
		/// Create single event
		/// </summary>
		/// <returns>the new event</returns>
		Task<Event> CreateEvent(Event yogaEvent);
	}
}
