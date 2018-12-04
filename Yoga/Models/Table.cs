using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Table
	{
		public int Id { get; set; }
		public string CaptainId { get; set; }
		public int Capacity { get; set; }
		//ICollection<User> Guests { get; set; }
	}
}
