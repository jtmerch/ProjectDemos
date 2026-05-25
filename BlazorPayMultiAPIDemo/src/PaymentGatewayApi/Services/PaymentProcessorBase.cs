using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

// Base class that all payment processors inherit from — defines the contract every processor must follow
public abstract class PaymentProcessorBase
{
    // Every processor needs to identify itself by name
    public abstract string ProcessorName { get; }

    // Every processor must implement this method to actually authorize a payment
    public abstract Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
