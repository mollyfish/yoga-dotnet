using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.AddressViewModels
{
	public class AddAddressViewModel
	{
		public Person Owner { get; set; }
		public PhysicalAddress newAddress { get; set; }
	}
}
