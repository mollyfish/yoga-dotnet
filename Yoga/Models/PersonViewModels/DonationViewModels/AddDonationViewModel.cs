using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.DonationViewModels
{
	public class AddDonationViewModel
	{
		public Person Donor { get; set; }
		public Donation newDonation { get; set; }
	}
}
