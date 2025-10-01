namespace NuveiPayBlazorDemo.Shared.NuveiSubmitModel
{
    public class NuvTransCCRequest
    {
        public string MerchantId { get; set; }
        public string MerchantSiteId { get; set; }
        public string ClientRequestId { get; set; }
        public string TimeStamp { get; set; }
        public string Checksum { get; set; }
    }
}
