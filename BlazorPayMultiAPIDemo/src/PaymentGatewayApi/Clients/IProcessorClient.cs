using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

public interface IProcessorClient
{
    Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
