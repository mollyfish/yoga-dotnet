using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class PhysicalAddress
	{
		public int Id { get; set; }
		public int? PersonId { get; set; }
		public string Title { get; set; }
		[Required(ErrorMessage = "Street Address is required")]
		public string StreetAddress { get; set; }
		public string StreetAddressCont { get; set; }
		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }
		[Required(ErrorMessage = "State is required")]
		public string State { get; set; }
		[MaxLength(5, ErrorMessage = "ZIP code must be 5 numbers long")]
		[MinLength(5, ErrorMessage ="ZIP code must be 5 numbers long")]
		[Required(ErrorMessage = "Zipcode is required")]
		public string ZipCode { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsPrimary { get; set; }
	}
}
