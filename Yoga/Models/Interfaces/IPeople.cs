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
		/// Get single person by Id
		/// </summary>
		/// <returns>Single person</returns>
		Task<Person> GetPerson(int? Id);

		/// <summary>
		/// Get most recently added single person by Full Name 
		/// </summary>
		/// <returns>Single person</returns>
		Task<Person> GetMostRecentPersonByName(string fullName);

		/// <summary>
		/// Get all people with first and last name matching parameters 
		/// </summary>
		/// <returns>list of people with given first and last names</returns>
		Task<IEnumerable<Person>> GetPersonByName(string firstName, string lastName);

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
