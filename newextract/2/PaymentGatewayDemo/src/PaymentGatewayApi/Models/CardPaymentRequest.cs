using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models;

public class CardPaymentRequest : PaymentRequestBase
{
    [Required]
    public string CardNumber { get; set; } = string.Empty;

    [Range(1, 12)]
    public int ExpirationMonth { get; set; }

    [Range(2026, 2100)]
    public int ExpirationYear { get; set; }

    [Required]
    public string CVV { get; set; } = string.Empty;

    [Required]
    public string CustomerName { get; set; } = string.Empty;
}
