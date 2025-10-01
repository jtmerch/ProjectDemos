namespace NuveiPayBlazorDemo.Shared.NuveiSubmitModel
{
    public class NuvCCRefundCardReq
    {
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
        public string clientRequestId { get; set; }
        public string clientUniqueId { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string relatedTransactionId { get; set; }
        public string timeStamp { get; set; }
        public string checksum { get; set; }
    }
}
