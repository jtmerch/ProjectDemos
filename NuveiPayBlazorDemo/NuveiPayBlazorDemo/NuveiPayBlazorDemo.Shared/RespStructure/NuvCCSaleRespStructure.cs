namespace NuveiPayBlazorDemo.Shared.RespStructure
{
    public class NuvCCSaleRespStructure
    {
        public string? orderId { get; set; }
        public string? userTokenId { get; set; }
        public string? isAFT { get; set; }
        public string? isAFTOverridden { get; set; }
        public PaymentOption? paymentOption { get; set; }
        public Card? card { get; set; }
        public string? transactionStatus { get; set; }
        public int gwErrorCode { get; set; }
        public int gwExtendedErrorCode { get; set; }
        public string? transactionType { get; set; }
        public string? transactionId { get; set; }
        public string? externalTransactionId { get; set; }
        public string? authCode { get; set; }
        public string? customData { get; set; }
        public FraudDetails? fraudDetails { get; set; }
        public string? sessionToken { get; set; }
        public string? clientUniqueId { get; set; }
        public long internalRequestId { get; set; }
        public string? status { get; set; }
        public int errCode { get; set; }
        public string? reason { get; set; }
        public string? merchantId { get; set; }
        public string? merchantSiteId { get; set; }
        public string? version { get; set; }
        public string? clientRequestId { get; set; }
        public string? merchantAdviceCode { get; set; }
    }

    public class PaymentOption
    {
        public string? userPaymentOptionId { get; set; }
        public Card? card { get; set; }
    }

    public class Card
    {
        public string? ccCardNumber { get; set; }
        public string? bin { get; set; }
        public string? last4Digits { get; set; }
        public string? ccExpMonth { get; set; }
        public string? ccExpYear { get; set; }
        public string? acquirerId { get; set; }
        public string? cvv2Reply { get; set; }
        public string? avsCode { get; set; }
        public string? cardType { get; set; }
        public string? cardBrand { get; set; }
        public ThreeD? threeD { get; set; }
    }

    public class FraudDetails
    {
        public string? finalDecision { get; set; }
    }

    public class ThreeD
    {
    }

}

