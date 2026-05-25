namespace PaymentGatewayApi.DTOs;

// What we get back from the Fraud API after it evaluates the transaction
public class FraudResult
{
    public int FraudScore { get; set; }                         // Risk score from 0 (clean) to 100 (definitely fraud)
    public string Recommendation { get; set; } = string.Empty; // "Approved", "Review", or "Declined"
    public string Message { get; set; } = string.Empty;        // Explanation of why this score was given
    public string CardNetwork { get; set; } = string.Empty;    // Card network detected from the card number
    public bool VelocityFlag { get; set; }                     // True if this card fired off too many transactions recently
    public List<string> ChecksPerformed { get; set; } = new(); // All the individual checks the fraud engine ran
}
