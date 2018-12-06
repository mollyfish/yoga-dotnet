using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class PhoneNumber
	{
		public int Id { get; set; }
		public int? PersonId { get; set; }
		public string Phone { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
