using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TSYSCertApi.Connections.TSYSSummit;
using TSYSCertApi.Connections.TSYSSummit.AuthDeactivate;
using TSYSCertApi.Models.TSYS.AuthDeactivatePOS;
using TSYSCertApi.Models.TSYS.CC;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSYSCertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MakePaymentController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        //// POST api/<AuthenticatePOSController>
        //[Authorize]
        //[HttpPost]
        //public IActionResult Post(AuthenticatePOSModel AuthPOSParams)
        //{


        //    var ProcessRequest = new AuthenticatePOS();
        //    ProcessRequest.ProcessRequest(AuthPOSParams, _configuration);


        //    IActionResult response;

        //    if (ProcessRequest.HasError == false)
        //    {
        //        response = Ok(ProcessRequest.ResponseObject);
        //    }
        //    else
        //    {
        //        response = StatusCode(StatusCodes.Status500InternalServerError, ProcessRequest.ResponseObject);
        //    }
        //    return response;
        //}








        // POST api/<MakePaymentController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(CardNotPresentModel AuthPOSParams)
        {
            var ProcessTSYSSummit = new SummitConn(_configuration);

            var XMLData = "H4.TSH950<SGREQ><A1>300000450300133</A1><A3>FECE64178DB82BC6454DD41F</A3><A10>10</A10><A12>C</A12><A15>10000</A15><A17>00001</A17><A18>5960</A18><A21><B1>5</B1><B2>0</B2><B3>S</B3><B4>0</B4><B5>0</B5><B6>6</B6><B9>Q</B9></A21><A25>5499740000000057</A25><A26>1231</A26><A32>5</A32><A34>FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF</A34><A41>SAMPLE</A41><A42>1000</A42><A73>HOLD</A73><A94>Y</A94><A124><B1>FFFFFFFFFFF</B1></A124></SGREQ>";

            ProcessTSYSSummit.ConnectToSummit(XMLData);

            string processorResponse = ProcessTSYSSummit.TransactionResponse;

            IActionResult response;  //initialized to Unauthorized
            response = Ok(new { response = processorResponse });

            return response;
        }


    }
}
