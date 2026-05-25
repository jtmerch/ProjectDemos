using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public class SimulatedPaymentProcessor : PaymentProcessorBase
{
    private readonly IProcessorClient _processorClient;

    public SimulatedPaymentProcessor(IProcessorClient processorClient)
    {
        _processorClient = processorClient;
    }

    public override string ProcessorName => "SimulatedProcessor";

    public override Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
        return _processorClient.AuthorizeAsync(request, merchant, cancellationToken);
    }
}
