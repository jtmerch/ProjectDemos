using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

// Contract for the client that calls the Fraud API
public interface IFraudClient
{
    // Send the card and merchant info to the fraud service and get back a score and recommendation
    Task<FraudResult> CheckFraudAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
