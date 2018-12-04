﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.AccountViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Phone]
		[Display(Name = "Phone Number (with area code)")]
		[MinLength(10)]
		public string PhoneNumber { get; set; }

		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }

		[Display(Name = "Street Address Cont.")]
		public string StreetAddressCont { get; set; }

		[Display(Name = "City")]
		public string City { get; set; }

		[Display(Name = "State")]
		public string State { get; set; }

		[Display(Name = "ZIP")]
		public string ZipCode { get; set; }

		[Required]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
