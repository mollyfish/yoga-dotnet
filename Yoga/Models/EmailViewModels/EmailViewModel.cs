using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.EmailViewModels
{
    public class EmailViewModel
    {
		public EmailAddress Email { get; set; }
		public Person Owner { get; set; }
	}
}
