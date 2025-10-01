namespace NuveiPayBlazorDemo.Shared.NuveiSubmitModel
{
    public class NuvCCSaleCardReq
    {
        public string sessionToken { get; set; }
        public string merchantId { get; set; }
        public string merchantSiteId { get; set; }
        public string clientRequestId { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string userTokenId { get; set; }
        public string clientUniqueId { get; set; } //Reference Number
        public PaymentOption paymentOption { get; set; }
        public DeviceDetails deviceDetails { get; set; }
        public BillingAddress billingAddress { get; set; }
        public string timeStamp { get; set; }
        public string checksum { get; set; }
    }

    public class PaymentOption
    {
        public Card card { get; set; }
    }

    public class Card
    {
        public string cardNumber { get; set; }
        public string cardHolderName { get; set; }
        public string expirationMonth { get; set; }
        public string expirationYear { get; set; }
        public string CVV { get; set; }
    }

    public class DeviceDetails
    {
        public string ipAddress { get; set; }
    }


    public class BillingAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
       // public string StreetNumber { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
    }

   

}
