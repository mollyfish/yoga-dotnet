using Yoga.Data;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.Services
{
	public class PeopleService : IPeople
	{
		private YogaDbContext _context;


		public PeopleService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<Person>> GetPeople()
		{
			return await _context.People.ToListAsync();
		}

		public async Task<Person> GetPerson(int Id)
		{
			var person = await _context.People.FindAsync(Id);
			var emails = _context.EmailAddresses.Where(e => e.PersonId == person.Id).ToList();
			person.EmailAddresses = emails;
			var phoneNumbers = _context.PhoneNumbers.Where(p => p.PersonId == person.Id).ToList();
			person.PhoneNumbers = phoneNumbers;
			var mailingAddresses = _context.PhysicalAddresses.Where(a => a.PersonId == person.Id).ToList();
			return person;
		}

		public async Task<Person> GetPerson(int? Id)
		{
			if (Id != null)
			{

				var person = await _context.People.FindAsync(Id);
				var emails = _context.EmailAddresses.Where(e => e.PersonId == person.Id).ToList();
				person.EmailAddresses = emails;
				var phoneNumbers = _context.PhoneNumbers.Where(p => p.PersonId == person.Id).ToList();
				person.PhoneNumbers = phoneNumbers;
				var mailingAddresses = _context.PhysicalAddresses.Where(a => a.PersonId == person.Id).ToList();
				return person;
			}
			else
			{
				Person person = new Person();
				return person;
			}
		}

		public async Task<Person> UpdatePerson(Person person)
		{
			_context.People.Update(person);
			await _context.SaveChangesAsync();
			return person;
		}

		public async Task<Person> CreatePerson(Person person)
		{
			await _context.People.AddAsync(person);
			await _context.SaveChangesAsync();
			return person;
		}

		public async Task<Person> GetMostRecentPersonByName(string fullName)
		{
			var personList = await _context.People.Where(p => p.FullName == fullName).OrderBy(p => p.DateAdded).ToListAsync();
			Person person = personList.Last();
			return person;
		}

		public async Task<IEnumerable<Person>> GetPersonByName(string firstName, string lastName)
		{
			var personList = await _context.People.Where(p => p.FirstName == firstName && p.LastName == lastName).ToListAsync();
			return personList;
		}
	}
}
