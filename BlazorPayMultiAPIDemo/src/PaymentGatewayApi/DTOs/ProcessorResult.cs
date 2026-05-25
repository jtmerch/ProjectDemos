namespace PaymentGatewayApi.DTOs;

// What comes back from the Processor API after it tries to authorize the card
public class ProcessorResult
{
    public string TransactionId { get; set; } = string.Empty;          // Unique ID assigned to this transaction
    public bool Approved { get; set; }                                  // True if the card was approved
    public string ProcessorResponseCode { get; set; } = string.Empty;  // Industry standard code ("00" = approved, "54" = expired, etc.)
    public string AuthCode { get; set; } = string.Empty;               // 6-digit auth code — only present on approvals
    public string Message { get; set; } = string.Empty;                // Description of what happened
    public string CardNetwork { get; set; } = string.Empty;            // Which card network was used
}
