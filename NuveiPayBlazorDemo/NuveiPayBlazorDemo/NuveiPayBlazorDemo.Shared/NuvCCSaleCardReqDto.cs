
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NuveiPayBlazorDemo.Shared
{
    public class NuvCCSaleCardReqDto
    {
        /// <summary>
        /// Nuvei-assigned ID identifying the merchant.
        /// </summary>
        [DefaultValue("3316684520456076214")]
        [Required]
        public string MerchantID { get; set; }

        /// <summary>
        /// Number displayed on card.
        /// </summary>
        [DefaultValue("4761344136141390")]
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// 2-digit expiration month displayed on  card, e.g. 02.
        /// </summary>
        [DefaultValue("01")]
        [Required]
        public string ExpirationMonth { get; set; }

        /// <summary>
        /// 2-digit expiration year displayed on  card, e.g. 02.
        /// </summary>
        [DefaultValue("30")]
        [Required]
        public string ExpirationYear { get; set; }

        /// <summary>
        /// Card Verification Value found on the card (CVV2, CVC2, CID)
        /// </summary>
        [DefaultValue("382")]
        [Required]
        public string CVV { get; set; }

        /// <summary>
        /// Dollar amount of transaction Format D.CC
        /// </summary>
        [DefaultValue("1.00")]
        [Required]
        public string TransactionAmount { get; set; } //CreditCardReturn, CreditCardReversal, CreditCardSale, CreditCardVoid      

        /// <summary>
        /// The 3-letter ISO currency code
        /// </summary>
        [DefaultValue("USD")]
        public string Currency { get; set; }

        /// <summary>
        /// First Name of the cardholder as is on their card account.
        /// </summary>
        [DefaultValue("Bill")]
        [Required]
        public string BillingFirstName { get; set; }

        /// <summary>
        /// Last Name of the cardholder as is on their card account.
        /// </summary>
        [DefaultValue("Ozura")]
        [Required]
        public string BillingLastName { get; set; }

        /// <summary>
        /// The email address used for billing purposes.
        /// </summary>
        [DefaultValue("test@ozura.io")]
        [Required]
        public string BillingEmail { get; set; }

        /// <summary>
        /// The billing street address (as is on customer's card account).
        /// </summary>
        [DefaultValue("100")]
        public string BillingAddress1 { get; set; }

        /// <summary>
        /// The billing address city (as is on customer's card account).
        /// </summary>
        [DefaultValue("Test City")]
        public string BillingCity { get; set; }

        /// <summary>
        /// The billing address 2-letter state (as is on customer's card account).
        /// </summary>
        [DefaultValue("FL")]
        public string BillingState { get; set; }

        /// <summary>
        /// The billing address zip code (as is on customer's card account).
        /// </summary>
        [DefaultValue("33606")]
        public string BillingZipcode { get; set; }

        /// <summary>
        ///  Phone. the cardholder's phone number.
        /// </summary>
        [DefaultValue("999-999-9999")]
        public string BillingPhone { get; set; }

        /// <summary>
        /// The billing address 2-letter country (as is on customer's card account).
        /// </summary>
        [DefaultValue("US")]
        public string BillingCountry { get; set; }

        //[DefaultValue("123451414")]
        //public string ReferenceNumber { get; set; } //map to clientUniqueId


        ///// <summary>
        /////  Original Transction Amount.  The transaction amount minus any Sales Tax or additional fee.  
        ///// </summary>
        //[DefaultValue("")]
        //public string OriginalAmount { get; set; }

        ///// <summary>
        /////  Sales Tax.  The added sales tax amount for this transaction if applicable.  
        ///// </summary>
        //[DefaultValue("")]
        //public string SalesTax { get; set; }

        ///// <summary>
        /////  Gas Fee.  Etherium Gas Fee for this transaction as calculated via our api call (if applicable).  
        ///// </summary>
        //[DefaultValue("")]
        //public string GasFee { get; set; }

    }
}
