using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.UFC.Common.ExceptionMiddleWare;
using Titan.UFC.Addresses.API.Resources;
using Titan.UFC.Addresses.API.Models;
using Titan.UFC.Addresses.API.Repository;


namespace Titan.UFC.Addresses.API.Service
{
    /// <summary>
    /// Address service to perform the
    /// address business validation
    /// </summary>
    public class AddressService : BaseService,IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ISharedResource _sharedLocalizer;
        /// <summary>
        /// Address service constructor to initialize 
        /// the repo and logger dependencies
        /// </summary>
        /// <param name="addressRepository"></param>
        /// <param name="sharedLocalizer"></param>
        public AddressService(IAddressRepository addressRepository, ISharedResource sharedLocalizer)
        {
            _addressRepository = addressRepository;
            _sharedLocalizer = sharedLocalizer;
        }
        /// <summary>
        /// Create address service
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> CreateAddress(Address address)
        {
            /*
             * Check the state and country code and state code is available
             */

            bool countryCodeAvailable = await _addressRepository.CheckCountryCode(address.CountryCode);

            if (!countryCodeAvailable)
            { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_Country_Code_Not_Exist]); }

            int stateId = await _addressRepository.CheckStateCode(address.CountryCode + "-" + address.StateCode);
            if(stateId==-1)
            {
                { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_State_Code_Not_Exist]); }
            }            
            address.StateID = stateId;
            return await _addressRepository.CreateAddress(address);
        }
        /// <summary>
        /// Delete the address information against
        /// the unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAddress(string uniqueAddressId)
        {
            bool isAddressAvailable = await _addressRepository.CheckAddressId(Guid.Parse(uniqueAddressId));
            if(!isAddressAvailable)
            { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_Id_NotFound]); }
            return await _addressRepository.DeleteAddress(Guid.Parse(uniqueAddressId));
        }
        /// <summary>
        /// Get the address information
        /// against the unique address id
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public async Task<Address> GetAddressById(string uniqueAddressId)
        {
            bool isAddressAvailable = await _addressRepository.CheckAddressId(Guid.Parse(uniqueAddressId));
            if (!isAddressAvailable)
            { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_Id_NotFound]); }
            return await _addressRepository.GetAddressById(Guid.Parse(uniqueAddressId));
        }
        /// <summary>
        /// Update the address
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> UpdateAddress(string uniqueAddressId, Address address)
        {
            bool isAddressAvailable = await _addressRepository.CheckAddressId(Guid.Parse(uniqueAddressId));
            if (!isAddressAvailable)
            { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_Id_NotFound]); }

            /*
             * Check the state and country code and state code is available
             */

            bool countryCodeAvailable = await _addressRepository.CheckCountryCode(address.CountryCode);

            if (!countryCodeAvailable)
            { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_Country_Code_Not_Exist]); }

            int stateId = await _addressRepository.CheckStateCode(address.CountryCode + "-" + address.StateCode);
            if (stateId == -1)
            {
                { throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Address_State_Code_Not_Exist]); }
            }
            address.StateID = stateId;

            return await _addressRepository.UpdateAddress(Guid.Parse(uniqueAddressId), address);            
        }
    }
}
