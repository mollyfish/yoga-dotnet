﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Models.Interfaces
{
	public interface IAddress
	{
		/// <summary>
		/// Get list of addresses
		/// </summary>
		/// <returns>List of addresses</returns>
		Task<IEnumerable<PhysicalAddress>> GetAddresses();

		/// <summary>
		/// Get single address by Id
		/// </summary>
		/// <returns>Single address</returns>
		Task<PhysicalAddress> GetPhysicalAddress(int Id);

		/// <summary>
		/// Get single address by title
		/// </summary>
		/// <returns>Single address</returns>
		Task<PhysicalAddress> GetMostRecentPhysicalAddressByTitle(string title);

		/// <summary>
		/// Update single address
		/// </summary>
		/// <returns>the updated address</returns>
		Task<PhysicalAddress> UpdatePhysicalAddress(PhysicalAddress address);

		/// <summary>
		/// Create single address
		/// </summary>
		/// <returns>the new address</returns>
		Task<PhysicalAddress> CreatePhysicalAddress(PhysicalAddress address);
	}
}