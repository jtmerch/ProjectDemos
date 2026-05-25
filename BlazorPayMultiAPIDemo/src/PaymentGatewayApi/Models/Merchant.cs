namespace PaymentGatewayApi.Models;

// Represents a merchant account in our system
public class Merchant
{
    public string MerchantId { get; set; } = string.Empty;        // Unique ID like "M1001"
    public string MerchantName { get; set; } = string.Empty;      // Display name of the business
    public bool IsActive { get; set; }                             // If false, we won't process payments for them
    public string ProcessorName { get; set; } = string.Empty;     // Which payment processor handles their transactions
   
    // Low, Medium, or High — this is a label we assign to the merchant, not the transaction.
    // The fraud engine uses it to add points to the fraud score, so merchants tagged as High risk
    // will have their transactions scored more harshly than a Low risk merchant doing the same charge.
    public string RiskLevel { get; set; } = "Low";
}
