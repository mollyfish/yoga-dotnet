﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Class
	{
		public int Id { get; set; }
		public int InstructorId { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public int LocationId { get; set; }
		//ICollection<User> Students { get; set; }

	}
}
