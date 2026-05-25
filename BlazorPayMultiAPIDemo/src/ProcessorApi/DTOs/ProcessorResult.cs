namespace ProcessorApi.DTOs;

// The result the Processor API sends back after evaluating a card authorization
public class ProcessorResult
{
    public string TransactionId { get; set; } = string.Empty;          // Unique ID assigned to this transaction
    public bool Approved { get; set; }                                  // True if the card was approved
    public string ProcessorResponseCode { get; set; } = string.Empty;  // Standard code — "00" = approved, others = decline reason
    public string AuthCode { get; set; } = string.Empty;               // 6-digit code issued on approval
    public string Message { get; set; } = string.Empty;                // Description of the result
    public string CardNetwork { get; set; } = string.Empty;            // Which card network was used
}
