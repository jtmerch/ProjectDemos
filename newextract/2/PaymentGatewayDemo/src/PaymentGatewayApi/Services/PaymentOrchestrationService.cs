using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public class PaymentOrchestrationService : IPaymentOrchestrationService
{
    private readonly IMerchantService _merchantService;
    private readonly IFraudClient _fraudClient;
    private readonly PaymentProcessorBase _paymentProcessor;
    private readonly ILogger<PaymentOrchestrationService> _logger;

    public PaymentOrchestrationService(
        IMerchantService merchantService,
        IFraudClient fraudClient,
        PaymentProcessorBase paymentProcessor,
        ILogger<PaymentOrchestrationService> logger)
    {
        _merchantService = merchantService;
        _fraudClient = fraudClient;
        _paymentProcessor = paymentProcessor;
        _logger = logger;
    }

    public async Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, CancellationToken cancellationToken = default)
    {
        var merchant = await _merchantService.GetMerchantAsync(request.MerchantId, cancellationToken);

        if (merchant is null)
        {
            return new PaymentAuthorizationResponse
            {
                Approved = false,
                ResponseMessage = "Merchant not found.",
                ProcessorResponseCode = "MERCHANT_NOT_FOUND"
            };
        }

        if (!merchant.IsActive)
        {
            return new PaymentAuthorizationResponse
            {
                Approved = false,
                MerchantName = merchant.MerchantName,
                ProcessorName = merchant.ProcessorName,
                ResponseMessage = "Merchant is inactive.",
                ProcessorResponseCode = "MERCHANT_INACTIVE"
            };
        }

        _logger.LogInformation("Starting async fraud and processor calls for merchant {MerchantId}", merchant.MerchantId);

        var fraudTask = _fraudClient.CheckFraudAsync(request, merchant, cancellationToken);
        var processorTask = _paymentProcessor.AuthorizeAsync(request, merchant, cancellationToken);

        await Task.WhenAll(fraudTask, processorTask);

        var fraudResult = await fraudTask;
        var processorResult = await processorTask;

        if (fraudResult.Recommendation.Equals("Declined", StringComparison.OrdinalIgnoreCase) || fraudResult.FraudScore >= 80)
        {
            return new PaymentAuthorizationResponse
            {
                Approved = false,
                MerchantName = merchant.MerchantName,
                ProcessorName = merchant.ProcessorName,
                FraudScore = fraudResult.FraudScore,
                ProcessorResponseCode = processorResult.ProcessorResponseCode,
                AuthCode = processorResult.AuthCode,
                ResponseMessage = $"Declined by fraud rules. {fraudResult.Message}"
            };
        }

        return new PaymentAuthorizationResponse
        {
            Approved = processorResult.Approved,
            MerchantName = merchant.MerchantName,
            ProcessorName = merchant.ProcessorName,
            FraudScore = fraudResult.FraudScore,
            ProcessorResponseCode = processorResult.ProcessorResponseCode,
            AuthCode = processorResult.AuthCode,
            ResponseMessage = processorResult.Approved
                ? "Payment approved."
                : $"Payment declined by processor. {processorResult.Message}"
        };
    }
}
