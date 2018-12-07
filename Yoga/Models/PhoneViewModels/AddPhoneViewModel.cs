using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PhoneViewModels
{
	public class AddPhoneViewModel
	{
		public Person Owner { get; set; }
		public PhoneNumber newPhone { get; set; }
	}
}
