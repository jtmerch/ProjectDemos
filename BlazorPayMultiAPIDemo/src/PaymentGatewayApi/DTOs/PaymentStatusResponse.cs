namespace PaymentGatewayApi.DTOs;

public class PaymentStatusResponse
{
    public string TransactionId { get; set; } = string.Empty;
    public string Status { get; set; } = "Approved";
    public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;
}
