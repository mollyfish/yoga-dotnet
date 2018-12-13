using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.DonationViewModels
{
	public class DonationViewModel
	{
		public Donation	Donation { get; set; }
		public Person Donor { get; set; }
	}
}
