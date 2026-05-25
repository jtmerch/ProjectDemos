using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public interface IPaymentOrchestrationService
{
    Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, CancellationToken cancellationToken = default);
}
