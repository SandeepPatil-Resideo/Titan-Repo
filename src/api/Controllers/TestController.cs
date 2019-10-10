using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Titan.Common.Diagnostics.State;
using TitanTemplate.titanaddressapi.Diagnostics;
using Titan.Common.Services.Auditing.AspNetCore;

namespace TitanTemplate.titanaddressapi.Controllers
{
    [Route("api/Test")]
    public class TestController : Controller
    {
        protected IStateObserver StateObserver { get; }

        public TestController(IStateObserver stateObserver)
        {
            StateObserver = stateObserver;
        }

        [TitanAudit("Put request with string - {someString}")]
        [HttpPut("{someString}")]
        public async Task<IActionResult> PutAsync(string someString)
        {
            try
            {
                //Do something
                StateObserver.Success();
                return Ok();
            }
            catch(Exception e)
            {
                StateObserver.Failure(e);
                titanaddressapiEventSource.Log.PutAsyncError(e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}