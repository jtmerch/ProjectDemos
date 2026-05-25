using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

// This is our simulated processor — it wraps the HTTP client that calls the Processor API
public class SimulatedPaymentProcessor : PaymentProcessorBase
{
    private readonly IProcessorClient _processorClient;

    // The processor client is injected so we can swap it out easily for testing
    public SimulatedPaymentProcessor(IProcessorClient processorClient)
    {
        _processorClient = processorClient;
    }

    // Name used to identify this processor in logs and responses
    public override string ProcessorName => "SimulatedProcessor";

    // Hand off the authorization request to the processor client which makes the actual HTTP call
    public override Task<ProcessorResult> AuthorizeAsync(CardPaymentRequest request, Merchant merchant, CancellationToken cancellationToken = default)
    {
        return _processorClient.AuthorizeAsync(request, merchant, cancellationToken);
    }
}
