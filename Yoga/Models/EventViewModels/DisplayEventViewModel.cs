using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels
{
	public class DisplayEventViewModel
	{
		public Event Event { get; set; }
		public PhysicalAddress Location { get; set; }


		public IEnumerable<EventHost> HostIds { get; set; }
		public IEnumerable<Table> Tables { get; set; }
		public IEnumerable<TableCaptain> CaptainIds { get; set; }
		public IEnumerable<EventGuest> EventGuests { get; set; }
		public IEnumerable<IEnumerable<TableGuest>> GuestIdsByTable { get; set; }

	}
}
