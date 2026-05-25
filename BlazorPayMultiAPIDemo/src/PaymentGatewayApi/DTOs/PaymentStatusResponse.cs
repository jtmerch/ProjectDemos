namespace PaymentGatewayApi.DTOs;

// Response for the status check endpoint — shows current state of a transaction
public class PaymentStatusResponse
{
    public string TransactionId { get; set; } = string.Empty;         // The transaction we're looking up
    public string Status { get; set; } = "Approved";                  // Current status of the transaction
    public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;  // When this status was last set
}
