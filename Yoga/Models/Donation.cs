using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Donation
	{
		public int Id { get; set; }
		public string DonorId { get; set; }
		public string DonorDisplayName { get; set; }
		public bool DisplayAsAnonymous { get; set; }
		public int? EventId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Honoree { get; set; }
		public bool TaxReceiptSent { get; set; }
		public bool ThankYouSent { get; set; }
		public DateTime TaxReceiptSentDate { get; set; }
		public DateTime ThankYouSentDate { get; set; }
		public DonationType DonationType { get; set; }
	}

	public enum DonationType
	{
		cash,
		check,
		card
	}
}
