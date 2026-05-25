using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services.IService;

public interface IPaymentOrchestrationService
{
    Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, string correlationId, CancellationToken cancellationToken = default);
}
