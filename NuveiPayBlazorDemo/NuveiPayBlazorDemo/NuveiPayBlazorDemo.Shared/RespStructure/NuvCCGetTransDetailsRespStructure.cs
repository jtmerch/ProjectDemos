

namespace NuveiPayBlazorDemo.Shared.RespStructure
{
    public class NuvCCGetTransDetailsRespStructure
    {
        public long internalRequestId { get; set; }
        public string status { get; set; }
        public int errCode { get; set; }
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
        public string version { get; set; }
        public UserDetails userDetails { get; set; }
        public DeviceDetails deviceDetails { get; set; }
        public TransactionDetails transactionDetails { get; set; }
        public PaymentOption paymentOption { get; set; }
        public PartialApproval partialApproval { get; set; }
        public ProductDetails productDetails { get; set; }
        public FraudDetails fraudDetails { get; set; }
    }
    public class UserDetails
    {
        public string userTokenId { get; set; }
    }

    public class TransactionDetails
    {
        public DateTime date { get; set; }
        public DateTime originalTransactionDate { get; set; }
        public string transactionStatus { get; set; }
        public string transactionType { get; set; }
        public string authCode { get; set; }
        public string credited { get; set; }
        public string transactionId { get; set; }
        public string acquiringBankName { get; set; }
    }

    public class PartialApproval
    {
        public string requestedAmount { get; set; }
        public string requestedCurrency { get; set; }
    }

    public class ProductDetails
    {
        public string productId { get; set; }
    }

    public class DeviceDetails
    {
        public string ipAddress { get; set; }
    }

}
