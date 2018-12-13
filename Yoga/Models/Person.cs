using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Person
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string FullName { get; set; }
		public DateTime DateAdded { get; set; }

		public ICollection<PhysicalAddress> MailingAddresses { get; set; }
		public ICollection<EmailAddress> EmailAddresses { get; set; }
		public ICollection<PhoneNumber> PhoneNumbers { get; set; }
		public ICollection<UserMailingList> MailingLists { get; set; }
		public ICollection<Donation> Donations { get; set; }
		public ICollection<EventGuest> EventsAttended { get; set; }
		public ICollection<Class> ClassesAttended { get; set; }
		public ICollection<TableGuest> Tables { get; set; }
	}
}
