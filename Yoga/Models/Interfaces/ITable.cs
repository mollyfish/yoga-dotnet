using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.Interfaces
{
	public interface ITable
	{
		/// <summary>
		/// Get list of tables
		/// </summary>
		/// <returns>List of tables</returns>
		Task<IEnumerable<Table>> GetTables();

		/// <summary>
		/// Get single table by Id
		/// </summary>
		/// <returns>Single table</returns>
		Task<Table> GetTable(int Id);

		/// <summary>
		/// Get single table by id and capta
		/// </summary>
		/// <returns>Single table</returns>
		//Task<Table> GetMostRecentTableByName(string fullName);

		/// <summary>
		/// Update single table
		/// </summary>
		/// <returns>the updated table</returns>
		Task<Table> UpdateTable(Table table);

		/// <summary>
		/// Create single table
		/// </summary>
		/// <returns>the new table</returns>
		Task<Table> CreateTable(Table table);
	}
}
