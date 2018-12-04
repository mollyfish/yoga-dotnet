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

		IEnumerable<EventMailingList> EventMailingLists { get; set; }
		IEnumerable<UserMailingList> UserMailingLists { get; set; }
	}
}
