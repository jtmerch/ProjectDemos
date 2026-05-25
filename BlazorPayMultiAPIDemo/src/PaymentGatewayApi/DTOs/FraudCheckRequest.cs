namespace PaymentGatewayApi.DTOs;

// The data we send to the Fraud API when requesting a fraud check
public class FraudCheckRequest
{
    public string MerchantId { get; set; } = string.Empty;     // Who's accepting the payment
    public decimal Amount { get; set; }                         // Transaction amount — large amounts get higher scores
    public string CustomerName { get; set; } = string.Empty;   // Cardholder name
    public string CardNumber { get; set; } = string.Empty;     // Full card number for blacklist and network checks
    public string RiskLevel { get; set; } = string.Empty;      // Passed in from the merchant record so the fraud engine can factor in the merchant's risk tier
}
