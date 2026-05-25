using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public abstract class PaymentProcessorBase
{
    public abstract string ProcessorName { get; }
    public abstract Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default);
}
