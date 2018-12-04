using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.Interfaces
{
	public interface IVIP
	{
		/// <summary>
		/// Get list of VIP users
		/// </summary>
		/// <returns>List of VIP users</returns>
		Task<IEnumerable<VIP>> GetVIPs();

		/// <summary>
		/// Get single VIP user
		/// </summary>
		/// <returns>Single VIP user</returns>
		Task<VIP> GetVIP(int Id);

		/// <summary>
		/// Update single VIP user
		/// </summary>
		/// <returns>the updated VIP</returns>
		Task<VIP> UpdateVIP(VIP vip);

		/// <summary>
		/// Create single VIP user
		/// </summary>
		/// <returns>the new VIP</returns>
		Task<VIP> CreateVIP(VIP vip);
	}
}
