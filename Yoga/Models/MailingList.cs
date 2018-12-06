using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class MailingList
	{
		public int Id { get; set; }
		public string Title { get; set; }

		ICollection<EventMailingList> EventMailingLists { get; set; }
		ICollection<UserMailingList> UserMailingLists { get; set; }
	}
}
