using System.Net.Http.Json;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

// This client is responsible for calling the external Fraud API over HTTP
public class FraudClient : IFraudClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FraudClient> _logger;

    // HttpClient is injected and pre-configured with the Fraud API base URL from Program.cs
    public FraudClient(HttpClient httpClient, ILogger<FraudClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<FraudResult> CheckFraudAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
        // Build the request object with just the fields the Fraud API needs
        var fraudRequest = new FraudCheckRequest
        {
            MerchantId = request.MerchantId,
            Amount = request.Amount,
            CustomerName = request.CustomerName,
            CardNumber = request.CardNumber,
            RiskLevel = merchant.RiskLevel
        };

        _logger.LogInformation("Calling FraudApi for merchant {MerchantId}", request.MerchantId);

        // POST the fraud check request and wait for a response
        var response = await _httpClient.PostAsJsonAsync("/api/fraud/check", fraudRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        // Deserialize the response — if we somehow get nothing back, default to a declined result
        return await response.Content.ReadFromJsonAsync<FraudResult>(cancellationToken: cancellationToken)
               ?? new FraudResult { FraudScore = 100, Recommendation = "Declined", Message = "Fraud service returned no response." };
    }
}
