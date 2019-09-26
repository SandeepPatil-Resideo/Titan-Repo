using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.UFC.Common.ExceptionMiddleWare;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.Repository;

namespace TitanTemplate.titanaddressapi.Service
{
    /// <summary>
    /// Address service to perform the
    /// address business validation
    /// </summary>
    public class AddressService : BaseService,IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        /// <summary>
        /// Address service constructor to initialize 
        /// the repo and logger dependencies
        /// </summary>
        /// <param name="addressRepository"></param>
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        /// <summary>
        /// Create address service
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<Address> CreateAddress(Address address)
        {
            return _addressRepository.CreateAddress(address);
        }
        /// <summary>
        /// Delete the address information against
        /// the unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public Task<int> DeleteAddress(string uniqueAddressId)
        {
            return _addressRepository.DeleteAddress(Guid.Parse(uniqueAddressId));
        }
        /// <summary>
        /// Get the address information
        /// against the unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public Task<Address> GetAddressById(string uniqueAddressId)
        {
            return _addressRepository.GetAddressById(Guid.Parse(uniqueAddressId));
        }
        /// <summary>
        /// Update the address
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<Address> UpdateAddress(string uniqueAddressId, Address address)
        {
            return _addressRepository.UpdateAddress(Guid.Parse(uniqueAddressId), address);            
        }
    }
}
