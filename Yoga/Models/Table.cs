using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Table
	{
		public int Id { get; set; }
		[Required]
		public int EventId { get; set; }
		[Required]
		public string Title { get; set; }
		public int Capacity { get; set; }
		public DateTime DateAdded { get; set; }
		public TableCaptain Captain { get; set; }

		public ICollection<TableGuest> Guests { get; set; }
	}
}
