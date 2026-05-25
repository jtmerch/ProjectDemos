namespace PaymentBlazorClient.DTOs;

// What comes back from the Payment Gateway after processing a payment — used to display results in the UI
public class PaymentAuthorizationResponse
{
    public string TransactionId { get; set; } = string.Empty;          // Unique ID for this transaction
    public bool Approved { get; set; }                                  // True = approved, False = declined
    public string ResponseMessage { get; set; } = string.Empty;        // Message shown to the user explaining the result
    public string AuthCode { get; set; } = string.Empty;               // Auth code from the processor (only on approvals)
    public int FraudScore { get; set; }                                 // Risk score from the fraud engine (0–100)
    public string ProcessorResponseCode { get; set; } = string.Empty;  // Standard industry response code
    public string MerchantName { get; set; } = string.Empty;           // Name of the merchant that processed the payment
    public string ProcessorName { get; set; } = string.Empty;          // Which processor handled the transaction
}
