using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Titan.Common.Services.Auditing.AspNetCore;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TitanTemplate.titanaddressapi.Controllers
{
    [Route("api/v{version:apiVersion}/Address")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [TitanAudit("Get_address")]
        public async Task<IActionResult> Get(string id)
        {
            Address address= await _addressService.GetAddressById(id);
            return Ok(address);
        }

        // POST api/<controller>
        [HttpPost]
        [TitanAudit("Create_Address")]
        public async Task<IActionResult> Post([FromBody]Address address)
        {
            address.Uuid = Guid.NewGuid();
            Address addressResult = await _addressService.CreateAddress(address);
            return Ok(addressResult);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Address address)
        {
            Address updateAddress = await _addressService.UpdateAddress(id, address);
            return Ok(updateAddress);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //int deleted
        }
    }
}
