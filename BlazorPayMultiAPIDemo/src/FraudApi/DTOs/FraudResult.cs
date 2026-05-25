namespace FraudApi.DTOs;

// The result the Fraud API sends back after evaluating a transaction
public class FraudResult
{
    public int FraudScore { get; set; }                         // 0 = no risk, 100 = definite fraud
    public string Recommendation { get; set; } = string.Empty; // "Approved", "Review", or "Declined"
    public string Message { get; set; } = string.Empty;        // Plain-language reason for the score
    public string CardNetwork { get; set; } = string.Empty;    // Visa, Mastercard, Amex, Discover, or Unknown
    public bool VelocityFlag { get; set; }                     // True if too many recent transactions on this card
    public List<string> ChecksPerformed { get; set; } = new(); // Step-by-step breakdown of what was checked
}
