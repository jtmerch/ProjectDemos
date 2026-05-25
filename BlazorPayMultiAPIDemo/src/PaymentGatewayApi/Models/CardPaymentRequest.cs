using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models;

// A card payment request — extends the base with card-specific fields
public class CardPaymentRequest : PaymentRequestBase
{
    [Required]
    public string IdempotencyKey { get; set; } = string.Empty;   // Unique key per request so we don't charge twice

    [Required]
    public string CardNumber { get; set; } = string.Empty;        // Full card number

    [Range(1, 12)]
    public int ExpirationMonth { get; set; }                      // Card expiry month (1–12)

    [Range(2026, 2100)]
    public int ExpirationYear { get; set; }                       // Card expiry year

    [Required]
    public string CVV { get; set; } = string.Empty;               // Security code on the back of the card

    [Required]
    public string CustomerName { get; set; } = string.Empty;      // Name as it appears on the card
}
