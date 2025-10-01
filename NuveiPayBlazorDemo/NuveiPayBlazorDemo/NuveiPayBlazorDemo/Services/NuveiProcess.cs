using NuveiPayBlazorDemo.Services.Interface;
using NuveiPayBlazorDemo.Shared;
using NuveiPayBlazorDemo.Shared.NuveiSubmitModel;
using NuveiPayBlazorDemo.Shared.RespStructure;
using NuveiPayBlazorDemo.Shared.Utility;

namespace NuveiPayBlazorDemo.Services
{
    public class NuveiProcess : INuveiProcess
    {

        private ResponseDto _response;
        private readonly INuveiSQLMerchantData _sqlMerchantData;
        private readonly INuveiTransPOST _nuveiTransPOST;
       // private readonly INuveiTransRecord _nuveiTransRecord;
        private readonly ILogger<NuveiProcess> _logger;
       // private readonly ICacheService _cacheService;

        public NuveiProcess(INuveiSQLMerchantData sqlMerchantData,
       INuveiTransPOST nuveiTransPOST, 
     // INuveiTransRecord nuveiTransRecord,
       ILogger<NuveiProcess> logger
       //ICacheService cacheService
       )
        {
            _sqlMerchantData = sqlMerchantData;
            _response = new ResponseDto();
            _nuveiTransPOST = nuveiTransPOST;
           // _nuveiTransRecord = nuveiTransRecord;
            _logger = logger;
           // _cacheService = cacheService;

        }
        public async Task<ResponseDto> SubmitNuvCCTransAsync(string transMethod, object requestData, string tblUserId, string MerchantID, string ipAddress, bool isMappedTrans)
        {

            string url = "";
            string sessionURL = AppDefinitions.NuveiSessionURL;
            string transDetailsURL = AppDefinitions.NuveiTransactionDetailsURL;

            NuveiCC merchantDataObject;



            //GET_CACHE if record is found get cache
            //var cacheKey = $"{AppDefinitions.RedisKeyPrefix}{tblUserId}{MerchantID}_{AppDefinitions.SubmitCCTrans}Nuvei";
            //var cacheData = _cacheService.GetCache<NuveiCC>(cacheKey);

            //_logger.LogInformation($"NuveiProcess SubmitNuvCCTransAsync search cache data {ObjectToString.ConvObjectToString(cacheData)}");
            //if (cacheData != null)
            //{
            //    _logger.LogInformation($"NuveiProcess SubmitNuvCCTransAsync cache found {ObjectToString.ConvObjectToString(cacheData)}");

            //    merchantDataObject = cacheData;
            //}
            //else
            //{
                merchantDataObject = await _sqlMerchantData.GetCCByUniqueUserIdAsync(MerchantID, tblUserId);
                _logger.LogInformation($"NuveiProcess SubmitNuvCCTransAsync msee if merchant data exists by AcceptorID - {MerchantID}. Result: {ObjectToString.ConvObjectToString(merchantDataObject)}");

            //SET_CACHE if record is found set cache
            // var expirationTime = DateTimeOffset.Now.AddMinutes(AppDefinitions.ExpirationMinutes);
            var expirationTime = DateTimeOffset.Now.AddMinutes(10);
            // _cacheService.SetCache<NuveiCC>(cacheKey, merchantDataObject, expirationTime);

            _logger.LogInformation($"NuveiProcess SubmitNuvCCTransAsync successful set cache for {ObjectToString.ConvObjectToString(merchantDataObject)}, expires {expirationTime}");
                //END SET_CACHE
          //  }


            if (!string.IsNullOrWhiteSpace(merchantDataObject.merchantID) && !string.IsNullOrWhiteSpace(merchantDataObject.merchantSiteID))
            {
                ResponseDto transResponseDto = new ResponseDto();


                string merchantId = merchantDataObject.merchantID;
                string merchantSecretKey = merchantDataObject.merchantSecretKey;
                string merchantSiteId = merchantDataObject.merchantSiteID;




                switch (transMethod)
                {
                    case "CreditCardSale":

                        url = $"{AppDefinitions.NuveiCCAPI}payment.do";

                        if (requestData is NuveiTransCCRequestDto saleRequestDto)
                        {

                            ResponseDto sessionResponseDto = new ResponseDto();

                            string sessionClientRequestId = RandomNumbers.GenerateUniqueId(255);
                            string sessionTimeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                            string sessionCheckSum = RandomNumbers.GenerateSessionChecksum256Hash(merchantId, merchantSiteId, sessionClientRequestId, sessionTimeStamp, merchantSecretKey);

                            var sessionRequest = new NuvSessionTokenRequest
                            {
                                merchantId = merchantId,
                                merchantSiteId = merchantSiteId,
                                clientRequestId = sessionClientRequestId,
                                timeStamp = sessionTimeStamp,
                                checksum = sessionCheckSum
                            };

                            sessionResponseDto = await _nuveiTransPOST.SessionSendAsync(sessionRequest, sessionURL);

                            if (sessionResponseDto.IsSuccess == true)
                            {

                                var sessionToken = sessionResponseDto.Message;

                                string Currency = saleRequestDto.Currency;
                                if (string.IsNullOrWhiteSpace(Currency) == true)
                                {
                                    Currency = "USD";
                                }

                                string Amount = saleRequestDto.TransactionAmount;

                                string saleClientRequestId = RandomNumbers.GenerateUniqueId(255);
                                string clientUniqueId = RandomNumbers.GenerateUniqueId(45);
                                string userTokenId = RandomNumbers.GenerateUniqueId(255);

                                NuvCCSaleCardReq saleRequest = new NuvCCSaleCardReq();

                                saleRequest.sessionToken = sessionToken;
                                saleRequest.merchantId = merchantId;
                                saleRequest.merchantSiteId = merchantSiteId;
                                saleRequest.clientRequestId = saleClientRequestId;
                                saleRequest.amount = Amount;
                                saleRequest.currency = Currency;
                                saleRequest.userTokenId = userTokenId;
                                saleRequest.clientUniqueId = clientUniqueId;


                                var paymentOption = new NuveiPayBlazorDemo.Shared.NuveiSubmitModel.PaymentOption();
                                var paymentOptionCard = new NuveiPayBlazorDemo.Shared.NuveiSubmitModel.Card();

                                paymentOptionCard.cardNumber = saleRequestDto.CardNumber;
                                paymentOptionCard.cardHolderName = $"{saleRequestDto.BillingFirstName} {saleRequestDto.BillingLastName}";
                                paymentOptionCard.expirationMonth = saleRequestDto.ExpirationMonth;
                                paymentOptionCard.expirationYear = saleRequestDto.ExpirationYear;
                                paymentOptionCard.CVV = saleRequestDto.CVV;

                                paymentOption.card = paymentOptionCard;

                                saleRequest.paymentOption = paymentOption;


                                //parse street address
                                string billingAddress1 = saleRequestDto.BillingAddress1;
                                // var addressUtility = new AddressUtility();
                                //  AddressComponents parseAddress = addressUtility.ParseAddress(billingAddress1);
                                // var parseStreetNumber = parseAddress.StreetNumber;
                                //  var parseStreetName = parseAddress.StreetName;

                                var deviceDetails = new NuveiPayBlazorDemo.Shared.NuveiSubmitModel.DeviceDetails();

                                deviceDetails.ipAddress = ipAddress;
                                saleRequest.deviceDetails = deviceDetails;

                                var billingAddress = new NuveiPayBlazorDemo.Shared.NuveiSubmitModel.BillingAddress();
                                billingAddress.firstName = saleRequestDto.BillingFirstName;
                                billingAddress.lastName = saleRequestDto.BillingLastName;
                                billingAddress.email = saleRequestDto.BillingEmail;
                                //billingAddress.StreetNumber = parseStreetNumber;
                                billingAddress.address = billingAddress1;
                                billingAddress.city = saleRequestDto.BillingCity;
                                billingAddress.state = saleRequestDto.BillingState;
                                billingAddress.zip = saleRequestDto.BillingZipcode;
                                billingAddress.country = saleRequestDto.BillingCountry;
                                billingAddress.phone = saleRequestDto.BillingPhone;

                                saleRequest.billingAddress = billingAddress;

                                string saleTimeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                                saleRequest.timeStamp = saleTimeStamp;

                                string saleChecksum = RandomNumbers.GenerateSaleChecksum256Hash(merchantId, merchantSiteId, saleClientRequestId, Amount, Currency, saleTimeStamp, merchantSecretKey);
                                saleRequest.checksum = saleChecksum;


                                transResponseDto = await _nuveiTransPOST.SendAsync<NuvCCSaleCardReq, NuvCCSaleRespStructure>(saleRequest, url);


                            }
                            else
                            {
                                return sessionResponseDto;
                            }
                        }
                        else
                        {
                            transResponseDto.IsSuccess = false;
                            transResponseDto.Message = "Response has incorrect format";
                        }

                        break;

                    case "CreditCardReturn":

                        url = $"{AppDefinitions.NuveiCCAPI}refundTransaction.do";

                        if (requestData is NuveiTransCCRequestDto refundRequestDto)
                        {

                            string Currency = refundRequestDto.Currency;
                            if (string.IsNullOrWhiteSpace(Currency) == true)
                            {
                                Currency = "USD";
                            }

                            string amount = refundRequestDto.TransactionAmount;
                            string comment = $"return for userid: {tblUserId}, amount: {amount}";
                            string clientRequestId = RandomNumbers.GenerateUniqueId(255);
                            string clientUniqueId = RandomNumbers.GenerateUniqueId(45);
                            string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                            string relatedTransactionId = refundRequestDto.TransactionID;
                            string checkSum = RandomNumbers.GenerateRetChecksum256Hash(merchantId, merchantSiteId, clientRequestId, clientUniqueId, amount, Currency, relatedTransactionId, comment, timeStamp, merchantSecretKey);

                            NuvCCRefundCardReq refundRequest = new NuvCCRefundCardReq();

                            refundRequest.merchantId = merchantId;
                            refundRequest.merchantSiteId = merchantSiteId;
                            refundRequest.relatedTransactionId = refundRequestDto.TransactionID;
                            refundRequest.amount = amount;
                            refundRequest.clientRequestId = clientRequestId;
                            refundRequest.clientUniqueId = clientUniqueId;

                            refundRequest.currency = Currency;

                            refundRequest.timeStamp = timeStamp;
                            refundRequest.checksum = checkSum;

                            transResponseDto = await _nuveiTransPOST.SendAsync<NuvCCRefundCardReq, NuvCCReturnRespStructure>(refundRequest, url);

                        }
                        else
                        {
                            transResponseDto.IsSuccess = false;
                            transResponseDto.Message = "Response has incorrect format";
                        }

                        break;

                    case "CreditCardVoid":

                        url = $"{AppDefinitions.NuveiCCAPI}voidTransaction.do";

                        if (requestData is NuveiTransCCRequestDto voidRequestDto)
                        {

                            string Currency = voidRequestDto.Currency;
                            if (string.IsNullOrWhiteSpace(Currency) == true)
                            {
                                Currency = "USD";
                            }

                            string amount = voidRequestDto.TransactionAmount;
                            string comment = $"return for userid: {tblUserId}, amount: {amount}";
                            string clientRequestId = RandomNumbers.GenerateUniqueId(255);
                            string clientUniqueId = RandomNumbers.GenerateUniqueId(45);
                            string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                            string relatedTransactionId = voidRequestDto.TransactionID;
                            string checkSum = RandomNumbers.GenerateRetChecksum256Hash(merchantId, merchantSiteId, clientRequestId, clientUniqueId, amount, Currency, relatedTransactionId, comment, timeStamp, merchantSecretKey);


                            NuvCCVoidCardReq voidRequest = new NuvCCVoidCardReq();

                            voidRequest.merchantId = merchantId;
                            voidRequest.merchantSiteId = merchantSiteId;
                            voidRequest.relatedTransactionId = voidRequestDto.TransactionID;
                            voidRequest.amount = amount;
                            voidRequest.clientRequestId = clientRequestId;
                            voidRequest.clientUniqueId = clientUniqueId;

                            voidRequest.currency = Currency;

                            voidRequest.timeStamp = timeStamp;
                            voidRequest.checksum = checkSum;

                            transResponseDto = await _nuveiTransPOST.SendAsync<NuvCCVoidCardReq, NuvCCVoidRespStructure>(voidRequest, url);

                        }
                        else
                        {
                            transResponseDto.IsSuccess = false;
                            transResponseDto.Message = "Response has incorrect format";
                        }

                        break;


                    case "getTransactionDetails":
                        if (requestData is NuveiTransCCRequestDto detailsRequestDto)
                        {
                            string transactionId = detailsRequestDto.TransactionID;
                            string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                            string checkSum = RandomNumbers.GenerateDetailChecksum256Hash(merchantId, merchantSiteId, transactionId, timeStamp, merchantSecretKey);

                            NuvCCGetTransactionDetails detailsRequest = new NuvCCGetTransactionDetails();

                            detailsRequest.merchantId = merchantId;
                            detailsRequest.merchantSiteId = merchantSiteId;
                            detailsRequest.transactionId = transactionId;
                            detailsRequest.timeStamp = timeStamp;
                            detailsRequest.checksum = checkSum;

                            transResponseDto = await _nuveiTransPOST.SendAsync<NuvCCGetTransactionDetails, NuvCCGetTransDetailsRespStructure>(detailsRequest, transDetailsURL);
                        }
                        else
                        {
                            transResponseDto.IsSuccess = false;
                            transResponseDto.Message = "Response has incorrect format";
                        }
                        break;

                    default:


                        transResponseDto.IsSuccess = false;
                        transResponseDto.Message = "Invalid transaction method provided";

                        break;
                }

                return transResponseDto;

            }
            else
            {
                //_cacheService.RemoveCache(cacheKey);
               // _cacheService.RemoveCache($"{AppDefinitions.RedisKeyPrefix}{tblUserId}{MerchantID}_sqlNuvei{AppDefinitions.MerchCCData}");


                _response.IsSuccess = false;
                _response.Message = "Merchant Information has not been found.";

                return _response;

            }


        }
    }
}
