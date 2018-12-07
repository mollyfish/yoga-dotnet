using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class PhysicalAddress
	{
		public int Id { get; set; }
		public int? PersonId { get; set; }
		public string Title { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddressCont { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsPrimary { get; set; }
	}
}
