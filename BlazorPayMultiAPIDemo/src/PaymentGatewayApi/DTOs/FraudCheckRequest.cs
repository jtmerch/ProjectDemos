namespace PaymentGatewayApi.DTOs;

public class FraudCheckRequest
{
    public string MerchantId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
}
