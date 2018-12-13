using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.PersonViewModels.DonationViewModels;

namespace Yoga.Models.PersonViewModels.DonationViewModels
{
	public class FilterableDonationViewModel
	{
		public IEnumerable<DonationViewModel> ListOfDvms { get; set; }
		public string searchString { get; set; }
		public string sortOrder { get; set; }
	}
}
