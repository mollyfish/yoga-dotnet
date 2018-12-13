using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.PersonViewModels.DonationViewModels
{
	public class DisplayDonationsViewModel
	{
		public ICollection<DonationViewModel> DonationList { get; set; }
	}
}
