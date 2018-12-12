using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.AddressViewModels
{
	public class AddressViewModel
	{
		public PhysicalAddress Address { get; set; }
		public Person Owner { get; set; }
	}
}
