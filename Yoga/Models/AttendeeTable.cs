using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class AttendeeTable
	{
		public string UserId { get; set; }
		public int TableId { get; set; }
		public int EventId { get; set; }
	}
}
