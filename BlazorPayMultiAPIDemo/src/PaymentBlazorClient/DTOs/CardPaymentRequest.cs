using System.ComponentModel.DataAnnotations;

namespace PaymentBlazorClient.DTOs;

// The form data the user fills out on the payment page — pre-populated with test values
public class CardPaymentRequest
{
    [Required]
    public string IdempotencyKey { get; set; } = string.Empty;   // Generated automatically before each submission to prevent duplicates

    // Test defaults so the form is ready to go on load
    public string MerchantId { get; set; } = "M1001";
    public decimal Amount { get; set; } = 125.50m;
    public string Currency { get; set; } = "USD";
    public string CardNumber { get; set; } = "4111111111111111";  // Standard Visa test card number
    public int ExpirationMonth { get; set; } = 12;
    public int ExpirationYear { get; set; } = 2030;
    public string CVV { get; set; } = "123";
    public string CustomerName { get; set; } = "Test Customer";
}
