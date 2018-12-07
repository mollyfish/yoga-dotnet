using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EmailViewModels
{
    public class AddEmailViewModel
    {
		public Person Owner { get; set; }
		public EmailAddress newEmail { get; set; }
	}
}
