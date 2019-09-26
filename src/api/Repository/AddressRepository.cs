using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.UFC.Common.ExceptionMiddleWare;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.Models;

namespace TitanTemplate.titanaddressapi.Repository
{
    /// <summary>
    /// Address repository 
    /// </summary>
    public class AddressRepository:IAddressRepository
    {
        private readonly AddressContext _addressContext;
        private readonly DbSet<AddressEntity> _addressEntity;
        private readonly IMapper _mapper;
        /// <summary>
        /// Address repo constructor to
        /// initialize the dependencies
        /// </summary>
        public AddressRepository(AddressContext addressContext, IMapper mapper)
        {
            _addressContext = addressContext;
            _addressEntity = _addressContext.Set<AddressEntity>();
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
            var address = await _addressEntity.Where(a=>a.Uuid== uniqueAddressId).FirstOrDefaultAsync();
            if(address==null)
            {
                throw new TitanCustomException(500, "Address not found");
            }
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
            try
            {
                var address =await _addressEntity.Where(a => a.Uuid == uniqueAddressId).FirstOrDefaultAsync();
                if (address == null)
                {
                    throw new TitanCustomException(404, "Address not found");
                }
                return _mapper.Map<Address>(address);
            }
            catch(Exception ex)
            {

            }
            return null;
            //return _mapper.Map<Address>(address);
        }
        /// <summary>
        /// Update address entity
        /// </summary>
        /// <param name="uniqueAddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> UpdateAddress(Guid uniqueAddressId, Address address)
        {
            var addressEntity = _addressEntity.SingleOrDefault(a => a.Uuid == uniqueAddressId);
            if(addressEntity == null)
            {
                throw new TitanCustomException(404, "Address not found");
            }
            var addressEntityObject = _mapper.Map<AddressEntity>(address);
            _addressContext.Update(addressEntityObject);
            await _addressContext.SaveChangesAsync();
            return address;
        }
    }
}
