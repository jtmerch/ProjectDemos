namespace ProcessorApi.Models;

// A record of a processed transaction — saved after every authorization attempt, approved or not
public class TransactionRecord
{
    public string TransactionId { get; set; } = string.Empty;      // Unique ID for this transaction
    public string MerchantId { get; set; } = string.Empty;         // Which merchant ran the transaction
    public decimal Amount { get; set; }                             // Charge amount
    public string Currency { get; set; } = string.Empty;           // Currency code
    public string CardNumberMasked { get; set; } = string.Empty;   // Last 4 digits only — never store the full number
    public string CardNetwork { get; set; } = string.Empty;        // Visa, Mastercard, Amex, etc.
    public bool Approved { get; set; }                             // Whether the transaction was approved
    public string ResponseCode { get; set; } = string.Empty;       // Standard response code
    public string AuthCode { get; set; } = string.Empty;           // Authorization code (approvals only)
    public DateTime Timestamp { get; set; }                        // When the transaction was processed (UTC)
}
