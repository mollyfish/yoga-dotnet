using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Yoga.Models
{
	// Add profile data for application users by adding properties to the User class
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }

		IEnumerable<PhysicalAddress> MailingAddresses { get; set; }
		IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
		IEnumerable<UserMailingList> MailingLists { get; set; }
		IEnumerable<Donation> Donations { get; set; }
		IEnumerable<Event> EventsAttended { get; set; }
		IEnumerable<Class> ClassesAttended { get; set; }

	}

}
