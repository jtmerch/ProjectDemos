using NuveiPayBlazorDemo.Shared;

namespace NuveiPayBlazorDemo.Services.Interface
{
    public interface INuveiProcess
    {
        Task<ResponseDto> SubmitNuvCCTransAsync(string transMethod, object requestData, string tblUserId, string MerchantID, string ipAddress, bool isMappedTrans);
    }
}
