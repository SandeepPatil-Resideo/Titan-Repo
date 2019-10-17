using System.Threading.Tasks;
using Titan.Ufc.Addresses.API.Models;

namespace Titan.Ufc.Addresses.API.Service
{
    /// <summary>
    /// Address Interface
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// Get the address details with the given
        /// unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        Task<Address> GetAddressById(string uniqueAddressId);
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
        Task<Address> UpdateAddress(string uniqueAddressId, Address address);
        /// <summary>
        /// Delete the address
        /// details 
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        Task<int> DeleteAddress(string uniqueAddressId);
    }
}
