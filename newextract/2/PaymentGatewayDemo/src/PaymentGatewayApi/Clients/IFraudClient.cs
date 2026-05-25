using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

public interface IFraudClient
{
    Task<FraudResult> CheckFraudAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
