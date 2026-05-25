namespace PaymentGatewayApi.DTOs;

public class PaymentAuthorizationResponse
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString("N");
    public bool Approved { get; set; }
    public string ResponseMessage { get; set; } = string.Empty;
    public string AuthCode { get; set; } = string.Empty;
    public int FraudScore { get; set; }
    public string ProcessorResponseCode { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public string ProcessorName { get; set; } = string.Empty;
    public string CardNetwork { get; set; } = string.Empty;
    public bool VelocityFlag { get; set; }
    public List<string> FraudChecks { get; set; } = new();
}
