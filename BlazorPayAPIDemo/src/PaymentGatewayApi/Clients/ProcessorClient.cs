using System.Net.Http.Json;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

public class ProcessorClient : IProcessorClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProcessorClient> _logger;

    public ProcessorClient(HttpClient httpClient, ILogger<ProcessorClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
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

        var response = await _httpClient.PostAsJsonAsync("/api/processor/authorize", processorRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProcessorResult>(cancellationToken: cancellationToken)
               ?? new ProcessorResult { Approved = false, ProcessorResponseCode = "96", Message = "Processor returned no response." };
    }
}
