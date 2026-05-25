namespace FraudApi.DTOs;

// The data the Fraud API receives when it's asked to evaluate a transaction
public class FraudCheckRequest
{
    public string MerchantId { get; set; } = string.Empty;     // Which merchant is involved
    public decimal Amount { get; set; }                         // Transaction amount — affects risk scoring
    public string CustomerName { get; set; } = string.Empty;   // Cardholder name
    public string CardNumber { get; set; } = string.Empty;     // Full card number for blacklist and network detection
    public string RiskLevel { get; set; } = string.Empty;      // Passed in from the merchant record so the fraud engine can factor in the merchant's risk tier
}
