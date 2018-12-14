using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels.GuestViewModels
{
	public class AddGuestViewModel
	{
		public EventGuest newGuest { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Event Event { get; set; }
		public Table Table { get; set; }
		public string ErrorMsg { get; set; }
	}
}
