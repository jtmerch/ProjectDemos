using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TSYSCertApi.Connections.TSYSSummit.AuthDeactivate;
using TSYSCertApi.Models.TSYS.AuthDeactivatePOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSYSCertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatePOSController : ControllerBase
    {

        private IConfiguration _configuration;

        public AuthenticatePOSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        // POST api/<AuthenticatePOSController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(AuthenticatePOSModel AuthPOSParams)
        {


            //check this http://amoghnatu.net/2014/04/24/invoke-a-post-method-with-xml-request-message-in-c/
            //http://threecop1.blogspot.com/2012/06/c-how-can-i-calculate-longitudinal.html
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/efb10bd2-ae50-49da-99a8-d65ddc92dde3/problem-with-lrcchecksum?forum=csharpgeneral


            var ProcessRequest = new AuthenticatePOS();
            ProcessRequest.ProcessRequest(AuthPOSParams, _configuration);


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
