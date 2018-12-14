using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels.GuestViewModels
{
	public class DisplayEventGuestsViewModel
	{
		public EventViewModel Evm { get; set; }
		public IEnumerable<GuestViewModel> Guests { get; set; }
	}
}
