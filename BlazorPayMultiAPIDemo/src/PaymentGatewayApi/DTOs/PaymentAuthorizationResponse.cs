namespace PaymentGatewayApi.DTOs;

// The full response we send back to the client after running a payment through fraud and the processor
public class PaymentAuthorizationResponse
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString("N");  // Unique ID for this transaction
    public bool Approved { get; set; }                                          // True if the payment went through
    public string ResponseMessage { get; set; } = string.Empty;                // Human-readable result message
    public string AuthCode { get; set; } = string.Empty;                       // Authorization code from the processor (only on approvals)
    public int FraudScore { get; set; }                                         // 0–100 score from the fraud engine
    public string ProcessorResponseCode { get; set; } = string.Empty;          // Standard response code (e.g., "00" = approved)
    public string MerchantName { get; set; } = string.Empty;                   // Name of the merchant who accepted the payment
    public string ProcessorName { get; set; } = string.Empty;                  // Which processor handled it
    public string CardNetwork { get; set; } = string.Empty;                    // Visa, Mastercard, Amex, etc.
    public bool VelocityFlag { get; set; }                                      // True if this card had too many recent transactions
    public List<string> FraudChecks { get; set; } = new();                     // List of checks the fraud engine ran
}
