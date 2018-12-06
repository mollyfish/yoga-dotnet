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
			return await _context.People.FindAsync(Id);
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
	}
}
