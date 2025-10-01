namespace NuveiPayBlazorDemo.Shared.NuveiSubmitModel
{
    public class NuvSessionTokenRequest
    {
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
        public string clientRequestId { get; set; }
        public string timeStamp { get; set; }
        public string checksum { get; set; }
    }
}
