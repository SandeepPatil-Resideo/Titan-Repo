using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.Models;

namespace TitanTemplate.titanaddressapi.Mapper
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
            CreateMap<Address, AddressEntity>();
        }
    }
}
