﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.UFC.Common.ExceptionMiddleWare;
using Titan.Ufc.Addresses.API.Entities;
using Titan.Ufc.Addresses.API.Models;

namespace Titan.Ufc.Addresses.API.Repository
{
    /// <summary>
    /// Address repository 
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressContext _addressContext;
        private readonly DbSet<AddressEntity> _addressEntity;
        private readonly DbSet<CountryStateEntity> _countryStateEntities;
        private readonly IMapper _mapper;
        /// <summary>
        /// Address repo constructor to
        /// initialize the dependencies
        /// </summary>
        public AddressRepository(AddressContext addressContext, IMapper mapper)
        {
            _addressContext = addressContext;
            _addressEntity = _addressContext.Set<AddressEntity>();
            _countryStateEntities = _addressContext.Set<CountryStateEntity>();
            _mapper = mapper;
        } 
        /// <summary>
        /// Create the new address in the address
        /// table
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> CreateAddress(Address address)
        {

            var addressObject = _mapper.Map<AddressEntity>(address);
            await _addressContext.AddAsync(addressObject);
            await _addressContext.SaveChangesAsync();
            return address;
        }
        /// <summary>
        /// Delete the address
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAddress(Guid uniqueAddressId)
        {
            var address = await _addressEntity.Where(a=>a.AddressUID== uniqueAddressId).FirstOrDefaultAsync();            
            _addressContext.Remove(address);
             return await _addressContext.SaveChangesAsync();  
        }
        /// <summary>
        /// Get address information from 
        /// the address entity
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public async Task<Address> GetAddressById(Guid uniqueAddressId)
        {
            var address = await _addressEntity.Where(a => a.AddressUID == uniqueAddressId).FirstOrDefaultAsync();           
            return _mapper.Map<Address>(address);
        }

        /// <summary>
        /// Update address entity
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> UpdateAddress(Guid uniqueAddressId, Address address)
        {
            var addressEntity = _addressEntity.SingleOrDefault(a => a.AddressUID == uniqueAddressId);            
            addressEntity.Address1 = address.Address1;
            addressEntity.Address2 = address.Address2;
            addressEntity.AddressLine3 = address.AddressLine3;
            addressEntity.City = address.City;
            addressEntity.CountryCode = address.CountryCode;
            addressEntity.StateID = address.StateId;
            addressEntity.ContactName = address.ContactName;
            addressEntity.PinCode = address.PinCode;
            addressEntity.IsVerified = address.IsVerified;            
            _addressContext.Update(addressEntity);
            await _addressContext.SaveChangesAsync();           
            return await GetAddressById(uniqueAddressId);
        }

        /// <summary>
        /// Check the address id is available in the entity
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <returns></returns>
        public async Task<bool> CheckAddressId(Guid uniqueAddressId)
        {
            var addressEntity = await _addressEntity.SingleOrDefaultAsync(a => a.AddressUID == uniqueAddressId);
            if (addressEntity == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check the given country code
        /// is given in the table
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public async Task<bool> CheckCountryCode(string countryCode)
        {

            var countryCodeAvailable = await _countryStateEntities.Where(a => a.CountryCode == countryCode).FirstOrDefaultAsync();
            if (countryCodeAvailable == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the given country code
        /// is given in the table
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public async Task<int> CheckStateCode(string stateCode)
        {
            var stateCodeAvailable = await _countryStateEntities.Where(a => a.AbbreviatedName == stateCode).SingleOrDefaultAsync();
            if (stateCodeAvailable == null)
            {
                return -1;
            }
            return stateCodeAvailable.StateId;
        }
    }
}
