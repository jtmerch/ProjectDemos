namespace NuveiPayBlazorDemo.Shared
{
    public class NuveiTransCCRequestDto
    {
        public string MerchantID { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public string TransactionAmount { get; set; }

        public string Currency { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingEmail { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipcode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingCountry { get; set; }
        public string ReferenceNumber { get; set; } //map to clientUniqueId
        public string TransactionID { get; set; }

        //public string OriginalAmount { get; set; }
        //public string SalesTax { get; set; }
        //public string GasFee { get; set; }
    }
}
