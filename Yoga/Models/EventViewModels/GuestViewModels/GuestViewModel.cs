using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels.GuestViewModels
{
	public class GuestViewModel
	{
		public EventGuest Guest { get; set; }
		public Event Event { get; set; }
		public Table Table { get; set; }
		public string GuestName { get; set; }
	}
}
