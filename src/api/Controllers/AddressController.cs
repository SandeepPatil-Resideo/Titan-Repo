using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Titan.Common.Diagnostics.State;
using Titan.Common.Services.Auditing.AspNetCore;
using Titan.UFC.Common.ExceptionMiddleWare;
using Titan.Ufc.Addresses.API.Diagnostics;
using Titan.Ufc.Addresses.API.Entities;
using Titan.Ufc.Addresses.API.Resources;
using Titan.Ufc.Addresses.API.Models;
using Titan.Ufc.Addresses.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Titan.Ufc.Addresses.API.Controllers
{
    /// <summary>
    /// Address Controller class
    /// to perform the crud operation
    /// </summary>
    [Route("api/v{version:apiVersion}/Addresses")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly ISharedResource _sharedLocalizer;
        protected IStateObserver StateObserver { get; }
        private readonly IMapper _mapper;
        private readonly AddressContext _addressContext;
        private readonly DbSet<AddressEntity> _addressEntity;
        /// <summary>
        /// Address constructor to initialize the dependencies
        /// </summary>
        /// <param name="addressService"></param>
        /// <param name="sharedLocalizer"></param>
        /// <param name="stateObserver"></param>
        /// <param name="mapper"></param>
        /// <param name="addressContext"></param>
        public AddressController(IAddressService addressService, ISharedResource sharedLocalizer, 
            IStateObserver stateObserver, IMapper mapper, AddressContext addressContext)
        {
            _addressService = addressService;
            _sharedLocalizer = sharedLocalizer;
            StateObserver = stateObserver;
            _mapper = mapper;
            _addressContext = addressContext;
            _addressEntity = _addressContext.Set<AddressEntity>();
        }
        /// <summary>
        /// Get address details based on the 
        /// unique address id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Address_Get")]
        [TitanAudit("Get request with {address id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            CheckValidGuid(id);
            Address address = await _addressService.GetAddressById(id);
            return Ok(address);
        }

        // POST api/<controller>
        [HttpPost(Name = "Address_Post")]
        [TitanAudit("Post equest with address object")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, _sharedLocalizer[SharedResourceKeys.Address_Input_Validation]);
            }
            try
            {
                address.CreatedDate = DateTime.UtcNow;
                Address addressResult = await _addressService.CreateAddress(address);
                StateObserver.Success();
                return Ok(addressResult);
            }
            catch(TitanCustomException titanCustomException)
            {
                throw titanCustomException;
            }
            catch (Exception e)
            {
                StateObserver.Failure(e);
                titanaddressapiEventSource.Log.PostAsyncError(e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}", Name = "Address_Put")]
        [TitanAudit("Put request with {address id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(string id, [FromBody]Address address)
        {
            /*
             * Check the id is valid guid
             */ 
            CheckValidGuid(id);
            if (address == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, _sharedLocalizer[SharedResourceKeys.Address_Input_Validation]);
            }
            try
            {
                Address updateAddress = await _addressService.UpdateAddress(id, address);
                StateObserver.Success();
                return Ok(updateAddress);
            }
            catch(TitanCustomException titanCustomException)
            {
                throw titanCustomException;
            }
            catch (Exception e)
            {
                StateObserver.Failure(e);
                titanaddressapiEventSource.Log.PutAsyncError(e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}", Name = "Address_Delete")]
        [TitanAudit("Delete request with {address id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            /*
             * Check the id is valid guid
             */ 
            CheckValidGuid(id);
            
            await _addressService.DeleteAddress(id);
            return NoContent();
        }
        [HttpPatch("{id}", Name = "Address_Patch")]
        public async Task<IActionResult> Patch(string id, [FromBody]JsonPatchDocument<Address> addressPatch)
        {
            /*
             * Check the the id is valid guid
             */ 
            CheckValidGuid(id);
            AddressEntity addressEntity = _addressEntity.SingleOrDefault(a => a.AddressUID == Guid.Parse(id));
            Address address = _mapper.Map<Address>(addressEntity);
            addressPatch.ApplyTo(address);
            _mapper.Map(address, addressEntity);
            _addressContext.Update(addressEntity);
            await _addressContext.SaveChangesAsync();
            return Ok(address);
        }
        /// <summary>
        /// Check the requested id is valid guid or not
        /// </summary>
        /// <param name="id"></param>
        private void CheckValidGuid(string id)
        {
            Guid yourGuid;
            if (!Guid.TryParse(id, out yourGuid))
            {
                throw new TitanCustomException(_sharedLocalizer[SharedResourceKeys.Guid_Input_Validation]);
            }
        }
    }
}
