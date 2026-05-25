namespace ProcessorApi.Models;

public class TransactionRecord
{
    public string TransactionId { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string CardNumberMasked { get; set; } = string.Empty;
    public string CardNetwork { get; set; } = string.Empty;
    public bool Approved { get; set; }
    public string ResponseCode { get; set; } = string.Empty;
    public string AuthCode { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
