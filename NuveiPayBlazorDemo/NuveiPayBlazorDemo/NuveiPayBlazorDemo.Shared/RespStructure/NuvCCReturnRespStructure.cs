namespace NuveiPayBlazorDemo.Shared.RespStructure
{
    public class NuvCCReturnRespStructure
    {
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
        public long internalRequestId { get; set; }
        public string transactionId { get; set; }
        public string externalTransactionId { get; set; }
        public string status { get; set; }
        public string isAFT { get; set; }
        public string transactionStatus { get; set; }
        public string authCode { get; set; }
        public int errCode { get; set; }
        public string errReason { get; set; }
        public int paymentMethodErrorCode { get; set; }
        public string paymentMethodErrorReason { get; set; }
        public int gwErrorCode { get; set; }
        public string gwErrorReason { get; set; }
        public int gwExtendedErrorCode { get; set; }
        public string customData { get; set; }
        public string version { get; set; }
        public string merchantAdviceCode { get; set; }
    }
}
