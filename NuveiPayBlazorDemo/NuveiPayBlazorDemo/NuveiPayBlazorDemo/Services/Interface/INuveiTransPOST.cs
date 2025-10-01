using NuveiPayBlazorDemo.Shared;
using NuveiPayBlazorDemo.Shared.NuveiSubmitModel;

namespace NuveiPayBlazorDemo.Services.Interface
{
    public interface INuveiTransPOST
    {
        Task<ResponseDto?> SendAsync<TRequest, TResponse>(TRequest requestData, string url);
        Task<ResponseDto> SessionSendAsync(NuvSessionTokenRequest sessionRequest, string sessionUrl);

    }
}
