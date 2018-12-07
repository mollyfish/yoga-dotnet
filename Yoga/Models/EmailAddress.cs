using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class EmailAddress
	{
		public int Id { get; set; }
		public int PersonId { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsPrimary { get; set; }
	}
}
