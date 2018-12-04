using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Event
	{
		public int Id { get; set; }
		public PhysicalAddress Location { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
	}
}
