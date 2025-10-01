using NuveiPayBlazorDemo.Shared;

namespace NuveiPayBlazorDemo.Services.Interface
{
    public interface INuveiSQLMerchantData
    {
        Task<NuveiCC?> GetCCByUniqueUserIdAsync(string merchantID, string tblUserId);
    }
}
