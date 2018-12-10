using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class PhoneNumber
	{
		public int Id { get; set; }
		public int? PersonId { get; set; }
		[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid format")]
		[Required(ErrorMessage = "Phone number is required")]
		//[MinLength(10, ErrorMessage = "Phone number is not long enough - did you forget to include the area code?")]
		//[MaxLength(10, ErrorMessage = "Phone number is too long")]
		public string Phone { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsPrimary { get; set; }
	}
}
