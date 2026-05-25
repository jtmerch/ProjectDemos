namespace PaymentGatewayApi.DTOs;

// Everything the Processor API needs to attempt a card authorization
public class ProcessorAuthorizeRequest
{
    public string MerchantId { get; set; } = string.Empty;     // Merchant accepting the payment
    public decimal Amount { get; set; }                         // Charge amount
    public string Currency { get; set; } = "USD";              // Currency code
    public string CardNumber { get; set; } = string.Empty;     // Full card number
    public int ExpirationMonth { get; set; }                    // Card expiry month
    public int ExpirationYear { get; set; }                     // Card expiry year
    public string CVV { get; set; } = string.Empty;            // Card security code
    public string CustomerName { get; set; } = string.Empty;   // Name on the card
    public string ProcessorName { get; set; } = string.Empty;  // Which processor should handle this
}
