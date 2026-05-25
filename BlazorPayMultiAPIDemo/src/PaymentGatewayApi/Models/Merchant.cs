namespace PaymentGatewayApi.Models;

public class Merchant
{
    public string MerchantId { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string ProcessorName { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = "Low";
}
