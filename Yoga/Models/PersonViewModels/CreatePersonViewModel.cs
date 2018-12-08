using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class CreatePersonViewModel
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddressCont { get; set; }
		public string City { get; set; }
		[MinLength(2)]
		[MaxLength(2)]
		public string State { get; set; }
		[MinLength(5, ErrorMessage = "ZIP code must be 5 numbers long")]
		[MaxLength(5, ErrorMessage = "ZIP code must be 5 numbers long")]
		public string ZipCode { get; set; }
		[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid format")]
		public string PhoneNumber { get; set; }
	}
}

