using System.Net.Http.Json;
using PaymentBlazorClient.DTOs;

namespace PaymentBlazorClient.Services;

// This client handles all communication between the Blazor UI and the Payment Gateway API
public class PaymentGatewayClient
{
    private readonly HttpClient _httpClient;

    // HttpClient is injected and pre-configured with the gateway's base URL from Program.cs
    public PaymentGatewayClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Send the payment form data to the gateway and get back the authorization result
    public async Task<PaymentAuthorizationResponse?> AuthorizeAsync(CardPaymentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/payments/authorize", request);
        return await response.Content.ReadFromJsonAsync<PaymentAuthorizationResponse>();
    }
}
