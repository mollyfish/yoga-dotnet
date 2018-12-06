﻿using System;
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
		public string StreetAddress { get; set; }
		public string StreetAddressCont { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		[MinLength(2)]
		[MaxLength(2)]
		public string State { get; set; }
		[Required]
		[MinLength(5)]
		[MaxLength(5)]
		public string ZipCode { get; set; }
		[MinLength(10)]
		[MaxLength(10)]
		public string PhoneNumber { get; set; }
	}
}
