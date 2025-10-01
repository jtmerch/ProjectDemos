using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NuveiPayBlazorDemo.Shared
{
    public class NuvCCRefundCardReqDto
    {
        /// <summary>
        /// Nuvei-assigned ID identifying the merchant.
        /// </summary>
        [DefaultValue("3316684520456076214")]
        [Required]
        public string MerchantID { get; set; }

        /// <summary>
        ///  Unique identifier of the original approved transaction. 
        /// </summary>
        [DefaultValue("")]
        [Required]
        public string TransactionID { get; set; }

        /// <summary>
        /// Dollar amount of transaction Format D.CC
        /// </summary>
        [DefaultValue("1.00")]
        [Required]
        public string Amount { get; set; }

        /// <summary>
        /// The 3-letter ISO currency code
        /// </summary>
        [DefaultValue("USD")]
        [Required]
        public string Currency { get; set; }


    }
}
