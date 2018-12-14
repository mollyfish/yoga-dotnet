using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class EventGuest
	{
		public int EventId { get; set; }
		public int PersonId { get; set; }
		public string GuestName { get; set; }
		[DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }
	}
}
