using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.UploadFileViewModel
{
	public class UploadFileViewModel
	{
		public IFormFile Import { get; set; }
	}
}
