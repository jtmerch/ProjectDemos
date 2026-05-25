using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services.IService;

// Contract for the service that runs the full payment flow from start to finish
public interface IPaymentOrchestrationService
{
    // Takes a payment request, runs fraud check and processor auth, and returns the final result
    Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, string correlationId, CancellationToken cancellationToken = default);
}
