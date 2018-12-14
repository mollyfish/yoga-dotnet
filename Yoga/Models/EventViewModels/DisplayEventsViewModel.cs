using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels
{
	public class DisplayEventsViewModel
	{
		public ICollection<EventViewModel> EventList { get; set; }

	}
}
