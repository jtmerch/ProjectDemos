using Microsoft.AspNetCore.Mvc;
using NuveiPayBlazorDemo.Services.Interface;
using NuveiPayBlazorDemo.Shared;
using System.Net;
using System.Security.Claims;

namespace NuveiPayBlazorDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NuveiTransactionController : ControllerBase
    {
        private readonly INuveiProcess _nuveiProcess;

        public NuveiTransactionController(INuveiProcess nuveiProcess)
        {
            _nuveiProcess = nuveiProcess;
        }


        [HttpPost("CardSale")]
        public async Task<IActionResult> CardSale([FromBody] NuvCCSaleCardReqDto requestDto)
        {

            var ccResponseModel = new ResponseDto();

            var userId = Guid.NewGuid().ToString();


            string MerchantID = requestDto.MerchantID;

            var transCCRequestDTO = new NuveiTransCCRequestDto()
            {

                MerchantID = MerchantID,
                CardNumber = requestDto.CardNumber,
                ExpirationMonth = requestDto.ExpirationMonth,
                ExpirationYear = requestDto.ExpirationYear,
                CVV = requestDto.CVV,
                TransactionAmount = requestDto.TransactionAmount,
                Currency = requestDto.Currency,
                BillingFirstName = requestDto.BillingFirstName,
                BillingLastName = requestDto.BillingLastName,
                BillingEmail = requestDto.BillingEmail,
                BillingAddress1 = requestDto.BillingAddress1,
                BillingCity = requestDto.BillingCity,
                BillingState = requestDto.BillingState,
                BillingZipcode = requestDto.BillingZipcode,
                BillingPhone = requestDto.BillingPhone,
                BillingCountry = requestDto.BillingCountry,
                ReferenceNumber = "",
                TransactionID = ""
                //OriginalAmount = requestDto.OriginalAmount,
                //SalesTax = requestDto.SalesTax,
                //GasFee = requestDto.GasFee
            };

            var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }


            var responseModel = await _nuveiProcess.SubmitNuvCCTransAsync("CreditCardSale", transCCRequestDTO, userId, MerchantID, ipAddress, false);

            if (responseModel.IsSuccess != true)
            {
                return BadRequest(responseModel);
            }
            else
            {

                return Ok(responseModel);

            } //end if cc process is success

        }
    }
}