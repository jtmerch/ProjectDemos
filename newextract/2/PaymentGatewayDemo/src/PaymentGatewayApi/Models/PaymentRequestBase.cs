using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models;

public abstract class PaymentRequestBase
{
    [Required]
    public string MerchantId { get; set; } = string.Empty;

    [Range(0.01, 999999.99)]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; } = "USD";
}
