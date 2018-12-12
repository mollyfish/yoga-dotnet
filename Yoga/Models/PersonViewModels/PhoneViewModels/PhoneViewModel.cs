using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.PhoneViewModels
{
	public class PhoneViewModel
	{
		public PhoneNumber Phone { get; set; }
		public Person Owner { get; set; }
	}
}
