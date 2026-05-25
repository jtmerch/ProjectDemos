using System.Net.Http.Json;
using PaymentBlazorClient.DTOs;

namespace PaymentBlazorClient.Services;

public class PaymentGatewayClient
{
    private readonly HttpClient _httpClient;

    public PaymentGatewayClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentAuthorizationResponse?> AuthorizeAsync(CardPaymentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/payments/authorize", request);
        return await response.Content.ReadFromJsonAsync<PaymentAuthorizationResponse>();
    }
}
