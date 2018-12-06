using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public DateTime DateAdded { get; set; }

		public ICollection<PhysicalAddress> MailingAddresses { get; set; }
		public ICollection<PhoneNumber> PhoneNumbers { get; set; }
		public ICollection<UserMailingList> MailingLists { get; set; }
		public ICollection<Donation> Donations { get; set; }
		public ICollection<Event> EventsAttended { get; set; }
		public ICollection<Class> ClassesAttended { get; set; }
	}
}
