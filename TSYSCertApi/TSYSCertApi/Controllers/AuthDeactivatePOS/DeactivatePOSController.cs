using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TSYSCertApi.Connections.TSYSSummit.AuthDeactivate;
using TSYSCertApi.Models.TSYS.AuthDeactivatePOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSYSCertApi.Controllers.AuthDeactivatePOS
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeactivatePOSController : ControllerBase
    {


        private IConfiguration _configuration;

        public DeactivatePOSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        // POST api/<DeactivatePOSController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(DeactivatePOSModel DeactivatePOSParams)
        {

            var ProcessRequest = new DeactivatePOS();
            ProcessRequest.ProcessRequest(DeactivatePOSParams, _configuration);


            IActionResult response;

            if (ProcessRequest.HasError == false)
            {
                response = Ok(ProcessRequest.ResponseObject);
            }
            else
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ProcessRequest.ResponseObject);
            }
            return response;
        }






    }
}
