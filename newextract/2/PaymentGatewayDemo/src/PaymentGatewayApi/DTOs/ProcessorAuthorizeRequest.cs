namespace PaymentGatewayApi.DTOs;

public class ProcessorAuthorizeRequest
{
    public string MerchantId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string CardNumber { get; set; } = string.Empty;
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string CVV { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string ProcessorName { get; set; } = string.Empty;
}
