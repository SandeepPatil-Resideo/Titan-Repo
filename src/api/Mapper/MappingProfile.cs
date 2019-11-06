using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.UFC.Addresses.API.Entities;
using Titan.UFC.Addresses.API.Models;

namespace Titan.UFC.Addresses.API.Mapper
{
    /// <summary>
    /// Mapping profile class is used
    /// to map the address model to address entity
    /// </summary>
    public class MappingProfile: Profile
    {
        /// <summary>
        /// Map domai model and entities
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AddressEntity, Address>();
        }
    }
}
