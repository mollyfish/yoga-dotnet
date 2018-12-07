using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.Interfaces
{
	public interface IPeople
	{
		/// <summary>
		/// Get list of people
		/// </summary>
		/// <returns>List of people</returns>
		Task<IEnumerable<Person>> GetPeople();

		/// <summary>
		/// Get single person by Id
		/// </summary>
		/// <returns>Single person</returns>
		Task<Person> GetPerson(int Id);

		/// <summary>
		/// Get single person by Full Name and StreetAddress
		/// </summary>
		/// <returns>Single person</returns>
		Task<Person> GetMostRecentPersonByName(string fullName);

		/// <summary>
		/// Update single person
		/// </summary>
		/// <returns>the updated person</returns>
		Task<Person> UpdatePerson(Person person);

		/// <summary>
		/// Create single person
		/// </summary>
		/// <returns>the new person</returns>
		Task<Person> CreatePerson(Person person);
	}
}
