using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Clients;

// Contract for the client that calls the Processor API to authorize a payment
public interface IProcessorClient
{
    // Send the payment details to the processor and get back an approval or decline
    Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
