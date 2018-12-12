using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Models.PersonViewModels.PhoneViewModels;

namespace Yoga.Models.Interfaces
{
	public interface IPhoneNumber
	{
		/// <summary>
		/// Get list of phone numbers
		/// </summary>
		/// <returns>List of phone numbers</returns>
		Task<IEnumerable<PhoneNumber>> GetPhoneNumbers();

		/// <summary>
		/// Get list of Phone numbers by owner
		/// </summary>
		/// <param name="Id">owner Id</param>
		/// <returns>List of phone numbers that belong to the person with given id</returns>
		Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByOwner(int Id);

		/// <summary>
		/// Get single phone number by Id
		/// </summary>
		/// <returns>Single phone number</returns>
		Task<PhoneViewModel> GetPhoneNumber(int Id, int owner);

		/// <summary>
		/// Get single phoneNumber by title
		/// </summary>
		/// <returns>Single phoneNumber</returns>
		Task<PhoneNumber> GetMostRecentPhoneNumberByPhone(string title);

		/// <summary>
		/// Update single phoneNumber
		/// </summary>
		/// <returns>the updated phoneNumber</returns>
		Task<PhoneNumber> UpdatePhoneNumber(PhoneNumber phoneNumber);

		/// <summary>
		/// Create single phoneNumber
		/// </summary>
		/// <returns>the new phoneNumber</returns>
		Task<PhoneNumber> CreatePhoneNumber(PhoneNumber phoneNumber);
	}
}
