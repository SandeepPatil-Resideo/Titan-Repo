using System;
using System.Threading.Tasks;
using Titan.UFC.Addresses.API.Models;

namespace Titan.UFC.Addresses.API.Repository
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
        /// <summary>
        /// Check the country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<bool> CheckCountryCode(string countryCode);
        /// <summary>
        /// Chek the state code is available
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        Task<string> CheckStateCode(string stateCode);

    }
}
