using System.Net.Http.Json;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

public class FraudClient : IFraudClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FraudClient> _logger;

    public FraudClient(HttpClient httpClient, ILogger<FraudClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<FraudResult> CheckFraudAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
        var fraudRequest = new FraudCheckRequest
        {
            MerchantId = request.MerchantId,
            Amount = request.Amount,
            CustomerName = request.CustomerName,
            CardNumber = request.CardNumber,
            RiskLevel = merchant.RiskLevel
        };

        _logger.LogInformation("Calling FraudApi for merchant {MerchantId}", request.MerchantId);

        var response = await _httpClient.PostAsJsonAsync("/api/fraud/check", fraudRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FraudResult>(cancellationToken: cancellationToken)
               ?? new FraudResult { FraudScore = 100, Recommendation = "Declined", Message = "Fraud service returned no response." };
    }
}
