namespace PaymentGatewayApi.DTOs;

public class FraudResult
{
    public int FraudScore { get; set; }
    public string Recommendation { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
