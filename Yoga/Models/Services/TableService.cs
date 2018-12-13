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
	public class TableService : ITable
	{
		private YogaDbContext _context;


		public TableService(YogaDbContext context)
		{
			_context = context;

		}

		public async Task<IEnumerable<Table>> GetTables()
		{
			return await _context.Tables.ToListAsync();
		}

		public async Task<Table> GetTable(int Id)
		{
			var table = await _context.Tables.FindAsync(Id);
			return table;
		}

		public async Task<Table> UpdateTable(Table table)
		{
			_context.Tables.Update(table);
			await _context.SaveChangesAsync();
			return table;
		}

		public async Task<Table> CreateTable(Table table)
		{
			await _context.Tables.AddAsync(table);
			await _context.SaveChangesAsync();
			return table;
		}

		//public async Task<Table> GetMostRecentTableByName(string fullName)
		//{
		//	var tableList = await _context.Tables.Where(p => p.FullName == fullName).OrderBy(p => p.DateAdded).ToListAsync();
		//	Table table = tableList.Last();
		//	return table;
		//}
	}
}
