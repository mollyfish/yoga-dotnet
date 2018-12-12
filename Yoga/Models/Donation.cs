using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Donation
	{
		public int Id { get; set; }
		public int DonorId { get; set; }
		public string DonorDisplayName { get; set; }
		public bool DisplayAsAnonymous { get; set; }
		public int? EventId { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		[DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime Date { get; set; }
		public string Honoree { get; set; }
		public bool TaxReceiptSent { get; set; }
		public bool ThankYouSent { get; set; }
		public DateTime? TaxReceiptSentDate { get; set; }
		public DateTime? ThankYouSentDate { get; set; }
		[Required]
		public DonationType DonationType { get; set; }
	}

	public enum DonationType
	{
		cash,
		check,
		card
	}
}
