using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanTemplate.titanaddressapi.Models;

namespace TitanTemplate.titanaddressapi.Repository
{
    /// <summary>
    /// Address repo to get the address information
    /// </summary>
    public interface IAddressRepository
    {
        /// <summary>
        /// Get the address details with the given
        /// unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        Task<Address> GetAddressById(Guid uniqueAddressId);
        /// <summary>
        /// Create new address with
        /// the given address details
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<Address> CreateAddress(Address address);
        /// <summary>
        /// Update the address details with the given
        /// unique adderess id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<Address> UpdateAddress(Guid uniqueAddressId, Address address);
        /// <summary>
        /// Delete the address
        /// details 
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        Task<int> DeleteAddress(Guid uniqueAddressId);
        /// <summary>
        /// Check address is available
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        Task<bool> CheckAddressId(Guid uniqueAddressId);

    }
}
