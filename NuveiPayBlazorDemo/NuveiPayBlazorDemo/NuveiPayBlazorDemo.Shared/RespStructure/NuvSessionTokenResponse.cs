namespace NuveiPayBlazorDemo.Shared.RespStructure
{
    public class NuvSessionTokenResponse
    {
        public string SessionToken { get; set; }
        public long InternalRequestId { get; set; }
        public string Status { get; set; }
        public int ErrCode { get; set; }
        public string Reason { get; set; }
        public string MerchantId { get; set; }
        public string MerchantSiteId { get; set; }
        public string Version { get; set; }
        public string ClientRequestId { get; set; }
    }
}
