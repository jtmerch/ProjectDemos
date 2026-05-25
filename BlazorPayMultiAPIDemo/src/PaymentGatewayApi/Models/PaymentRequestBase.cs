using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models;

// Base class with the fields that every payment request needs, regardless of payment type
public abstract class PaymentRequestBase
{
    [Required]
    public string MerchantId { get; set; } = string.Empty;   // Which merchant is accepting this payment

    [Range(0.01, 999999.99)]
    public decimal Amount { get; set; }                        // How much to charge

    [Required]
    public string Currency { get; set; } = "USD";             // Currency code — defaults to USD
}
