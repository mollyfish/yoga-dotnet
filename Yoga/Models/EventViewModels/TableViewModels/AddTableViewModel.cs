using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EventViewModels.TableViewModels
{
	public class AddTableViewModel
	{
		public string EventName { get; set; }
		public ICollection<EventGuest> PossibleCaptains { get; set; }
		public Person Captain { get; set; }
		public Table newTable { get; set; }
	}
}
