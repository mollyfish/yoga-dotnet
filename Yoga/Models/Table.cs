using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Table
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public int Capacity { get; set; }
		public DateTime DateAdded { get; set; }

		//public ICollection<TableCaptain> Captains { get; set; }
		public ICollection<TableGuest> Guests { get; set; }
	}
}
