using System.Net.Http.Json;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

// This client handles the HTTP call out to the Processor API to actually authorize the card
public class ProcessorClient : IProcessorClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProcessorClient> _logger;

    // HttpClient is injected and pre-configured with the Processor API base URL from Program.cs
    public ProcessorClient(HttpClient httpClient, ILogger<ProcessorClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
        // Build the request object the Processor API expects — includes full card details
        var processorRequest = new ProcessorAuthorizeRequest
        {
            MerchantId = request.MerchantId,
            Amount = request.Amount,
            Currency = request.Currency,
            CardNumber = request.CardNumber,
            ExpirationMonth = request.ExpirationMonth,
            ExpirationYear = request.ExpirationYear,
            CVV = request.CVV,
            CustomerName = request.CustomerName,
            ProcessorName = merchant.ProcessorName
        };

        _logger.LogInformation("Calling ProcessorApi for merchant {MerchantId}", request.MerchantId);

        // POST to the processor and wait for authorization decision
        var response = await _httpClient.PostAsJsonAsync("/api/processor/authorize", processorRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        // Deserialize the result — if nothing comes back, decline as a safe default
        return await response.Content.ReadFromJsonAsync<ProcessorResult>(cancellationToken: cancellationToken)
               ?? new ProcessorResult { Approved = false, ProcessorResponseCode = "96", Message = "Processor returned no response." };
    }
}
