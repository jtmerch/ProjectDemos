namespace PaymentGatewayApi.DTOs;

public class FraudResult
{
    public int FraudScore { get; set; }
    public string Recommendation { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string CardNetwork { get; set; } = string.Empty;
    public bool VelocityFlag { get; set; }
    public List<string> ChecksPerformed { get; set; } = new();
}
