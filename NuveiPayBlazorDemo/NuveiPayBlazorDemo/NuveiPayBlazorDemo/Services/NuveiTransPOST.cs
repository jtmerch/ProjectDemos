using NuveiPayBlazorDemo.Services.Interface;
using NuveiPayBlazorDemo.Shared;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using NuveiPayBlazorDemo.Shared.NuveiSubmitModel;
using NuveiPayBlazorDemo.Shared.RespStructure;
using NuveiPayBlazorDemo.Shared.Utility;

namespace NuveiPayBlazorDemo.Services
{
    public class NuveiTransPOST : INuveiTransPOST
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ResponseDto _response;
        private readonly ILogger<NuveiTransPOST> _logger;
        public NuveiTransPOST(IHttpClientFactory httpClientFactory,
            ILogger<NuveiTransPOST> logger)
        {
            _httpClientFactory = httpClientFactory;
            _response = new ResponseDto();
            _logger = logger;
        }


        public async Task<ResponseDto> SendAsync<TRequest, TResponse>(TRequest requestData, string url)
        {
            _logger.LogInformation($"NuveiTransPOST SendAsync method invoked.");

            try
            {
                var jsonContent = JsonSerializer.Serialize(requestData);
                var body = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _logger.LogInformation($"Raw JSON Body: {await body.ReadAsStringAsync()}");

                HttpClient client = _httpClientFactory.CreateClient("NuveiTransaction");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                _logger.LogInformation($"NuveiTransPOST SendAsync httpclient client - {client}.");

                var response = await client.PostAsync(url, body);

                _logger.LogInformation($"NuveiTransPOST SendAsync client.PostAsync url - {url}");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var responseErrorCheck = JsonSerializer.Deserialize<NuvGeneralTransResponse>(responseContent);

                    _logger.LogInformation($"NuveiTransPOST SendAsync responseErrorCheck content - {ObjectToString.ConvObjectToString(responseErrorCheck)}");


                    if (responseErrorCheck != null && responseErrorCheck.status == "ERROR")
                    {

                        _response.IsSuccess = false;
                        _response.Result = responseErrorCheck;
                        _response.Message = $"Transaction failed: {responseErrorCheck.reason}";
                    }
                    else
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var responseData = JsonSerializer.Deserialize<TResponse>(responseContent, options);

                        _logger.LogInformation($"NuveiTransPOST SendAsync response content - {ObjectToString.ConvObjectToString(responseData)}");


                        if (responseData != null)
                        {
                            _response.Result = responseData;
                            _response.IsSuccess = true;
                            _response.Message = "Transaction successful";
                        }
                        else
                        {
                            _response.IsSuccess = false;
                            _response.Message = "Failed to parse response data.";
                        }

                    }


                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Transaction failed with status code: {response.StatusCode}";

                }


                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = "";
                _response.Message = ex.Message;

                _logger.LogError($"NuveiTransPOST SendAsync method exception {ex.Message}");

                return _response;
            }

        }

        public async Task<ResponseDto> SessionSendAsync(NuvSessionTokenRequest sessionRequest, string sessionUrl)
        {
            _logger.LogInformation($"NuveiTransPOST SessionSendAsync method invoked.");

            try
            {
                var sessionResponse = await SendAsync<NuvSessionTokenRequest, NuvSessionTokenResponse>(sessionRequest, sessionUrl);

                if (sessionResponse.IsSuccess && sessionResponse.Result is NuvSessionTokenResponse tokenResponse)
                {
                    _response.IsSuccess = true;
                    _response.Message = tokenResponse.SessionToken;

                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Result = sessionResponse.Result;
                    _response.Message = "Failed to obtain session token";
                }



                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = "";
                _response.Message = ex.Message;

                _logger.LogError($"NuveiTransPOST SendAsync method exception {ex.Message}");

                return _response;
            }

        }


    }
}
