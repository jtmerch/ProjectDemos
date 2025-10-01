namespace NuveiPayBlazorDemo.Shared.NuveiSubmitModel
{
    public class NuvCCGetTransactionDetails
    {
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
       // public string clientUniqueId { get; set; }
        public string transactionId { get; set; }
        public string timeStamp { get; set; }
        public string checksum { get; set; }
    }
}
