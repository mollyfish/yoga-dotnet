﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.PhoneViewModels
{
	public class AddPhoneViewModel
	{
		public Person Owner { get; set; }
		public PhoneNumber newPhone { get; set; }
	}
}
