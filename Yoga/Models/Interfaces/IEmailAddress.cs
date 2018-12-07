using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.EmailViewModels;

namespace Yoga.Models.Interfaces
{
	public interface IEmailAddress
	{
		/// <summary>
		/// Get list of emailAddresses
		/// </summary>
		/// <returns>List of emailAddresses</returns>
		Task<IEnumerable<EmailAddress>> GetEmailAddresses();

		/// <summary>
		/// Get list of emailAddresses by owner
		/// </summary>
		/// <param name="Id">owner Id</param>
		/// <returns>List of email addresses that belong to the person with given id</returns>
		Task<IEnumerable<EmailAddress>> GetEmailAddressesByOwner(int Id);

		/// <summary>
		/// Get single emailAddress by Id
		/// </summary>
		/// <returns>Single emailAddress</returns>
		Task<EmailViewModel> GetEmailAddress(int Id);

		/// <summary>
		/// Get single emailAddress by title
		/// </summary>
		/// <returns>Single emailAddress</returns>
		Task<EmailAddress> GetEmailAddressByValue(string value);

		/// <summary>
		/// Update single emailAddress
		/// </summary>
		/// <returns>the updated emailAddress</returns>
		Task<EmailAddress> UpdateEmailAddress(EmailAddress emailAddress);

		/// <summary>
		/// Create single emailAddress
		/// </summary>
		/// <returns>the new emailAddress</returns>
		Task<EmailAddress> CreateEmailAddress(EmailAddress emailAddress);
	}
}
