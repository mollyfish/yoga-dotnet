using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class EditPersonViewModel
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddressCont { get; set; }
		public string City { get; set; }
		[MinLength(2)]
		[MaxLength(2)]
		public string State { get; set; }
		[MinLength(5)]
		[MaxLength(5)]
		public string ZipCode { get; set; }
		[MinLength(10)]
		[MaxLength(10)]
		public string PhoneNumber { get; set; }
	}
}

