using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models
{
	public class Donation : IValidatableObject
	{
		public int Id { get; set; }
		public int DonorId { get; set; }
		public string DonorDisplayName { get; set; }
		public bool DisplayAsAnonymous { get; set; }
		public int? EventId { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		public string Honoree { get; set; }
		public bool TaxReceiptSent { get; set; }
		public bool ThankYouSent { get; set; }
		[DataType(DataType.Date)]
		public DateTime? TaxReceiptSentDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime? ThankYouSentDate { get; set; }
		[Required]
		public DonationType DonationType { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ThankYouSent && ThankYouSentDate == null)
			{
				yield return new ValidationResult("Please fill out the date when the thank you was sent");
			}

			if (TaxReceiptSent && TaxReceiptSentDate == null)
			{
				yield return new ValidationResult("Please fill out the date when the tax receipt was sent");
			}
		}
	}

	public enum DonationType
	{
		cash,
		check,
		card
	}
}
