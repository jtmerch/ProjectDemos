using System.Diagnostics;
using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services.IService;

namespace PaymentGatewayApi.Services;

// This service coordinates the entire payment flow — merchant lookup, fraud check, and processor authorization
public class PaymentOrchestrationService : IPaymentOrchestrationService
{
    private readonly IMerchantService _merchantService;
    private readonly IFraudClient _fraudClient;
    private readonly PaymentProcessorBase _paymentProcessor;
    private readonly ILogger<PaymentOrchestrationService> _logger;

    // All dependencies come in through the constructor via dependency injection
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

    public async Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, string correlationId, CancellationToken cancellationToken = default)
    {
        // Step 1 — look up the merchant to make sure they exist and are active
        _logger.LogInformation("[{CorrelationId}] Merchant lookup started merchantId={MerchantId}", correlationId, request.MerchantId);
        var merchant = await _merchantService.GetMerchantAsync(request.MerchantId, cancellationToken);

        // If we can't find the merchant, stop here and return an error
        if (merchant == null)
        {
            _logger.LogWarning("[{CorrelationId}] Merchant not found merchantId={MerchantId}", correlationId, request.MerchantId);
            return new PaymentAuthorizationResponse
            {
                Approved = false,
                ResponseMessage = "Merchant not found.",
                ProcessorResponseCode = "MERCHANT_NOT_FOUND"
            };
        }

        _logger.LogInformation("[{CorrelationId}] Merchant resolved name={MerchantName} active={IsActive} processor={ProcessorName} risk={RiskLevel}",
            correlationId, merchant.MerchantName, merchant.IsActive, merchant.ProcessorName, merchant.RiskLevel);

        // If the merchant account is disabled, we can't process the payment
        if (!merchant.IsActive)
        {
            _logger.LogWarning("[{CorrelationId}] Merchant inactive merchantId={MerchantId}", correlationId, merchant.MerchantId);
            return new PaymentAuthorizationResponse
            {
                Approved = false,
                MerchantName = merchant.MerchantName,
                ProcessorName = merchant.ProcessorName,
                ResponseMessage = "Merchant is inactive.",
                ProcessorResponseCode = "MERCHANT_INACTIVE"
            };
        }

        // Step 2 — fire off fraud check and processor authorization at the same time to save time
        _logger.LogInformation("[{CorrelationId}] Fraud check and processor submission started in parallel", correlationId);

        var fraudSw = Stopwatch.StartNew();
        var processorSw = Stopwatch.StartNew();

        // Both calls run in parallel — we don't wait for one to finish before starting the other
        var fraudTask = _fraudClient.CheckFraudAsync(request, merchant, cancellationToken);
        var processorTask = _paymentProcessor.AuthorizeAsync(request, merchant, cancellationToken);

        // Wait for both to finish before we move on
        await Task.WhenAll(fraudTask, processorTask);

        var fraudResult = await fraudTask;
        fraudSw.Stop();

        var processorResult = await processorTask;
        processorSw.Stop();

        _logger.LogInformation("[{CorrelationId}] Fraud check complete score={FraudScore} recommendation={Recommendation} elapsed={ElapsedMs}ms",
            correlationId, fraudResult.FraudScore, fraudResult.Recommendation, fraudSw.ElapsedMilliseconds);

        _logger.LogInformation("[{CorrelationId}] Processor response received code={ProcessorResponseCode} approved={Approved} transactionId={TransactionId} elapsed={ElapsedMs}ms",
            correlationId, processorResult.ProcessorResponseCode, processorResult.Approved, processorResult.TransactionId, processorSw.ElapsedMilliseconds);

        // Step 3 — if fraud says decline or the score is too high, we block the payment
        if (fraudResult.Recommendation.Equals("Declined", StringComparison.OrdinalIgnoreCase) || fraudResult.FraudScore >= 80)
        {
            _logger.LogWarning("[{CorrelationId}] Authorization declined by fraud rules score={FraudScore} recommendation={Recommendation} transactionId={TransactionId}",
                correlationId, fraudResult.FraudScore, fraudResult.Recommendation, processorResult.TransactionId);

            return new PaymentAuthorizationResponse
            {
                TransactionId = processorResult.TransactionId,
                Approved = false,
                MerchantName = merchant.MerchantName,
                ProcessorName = merchant.ProcessorName,
                FraudScore = fraudResult.FraudScore,
                ProcessorResponseCode = processorResult.ProcessorResponseCode,
                AuthCode = processorResult.AuthCode,
                ResponseMessage = $"Declined by fraud rules. {fraudResult.Message}",
                CardNetwork = fraudResult.CardNetwork,
                VelocityFlag = fraudResult.VelocityFlag,
                FraudChecks = fraudResult.ChecksPerformed
            };
        }

        // Step 4 — fraud passed, so return whatever the processor decided
        var outcome = processorResult.Approved ? "Approved" : "Declined";
        _logger.LogInformation("[{CorrelationId}] Authorization complete outcome={Outcome} authCode={AuthCode} transactionId={TransactionId}",
            correlationId, outcome, processorResult.AuthCode, processorResult.TransactionId);

        return new PaymentAuthorizationResponse
        {
            TransactionId = processorResult.TransactionId,
            Approved = processorResult.Approved,
            MerchantName = merchant.MerchantName,
            ProcessorName = merchant.ProcessorName,
            FraudScore = fraudResult.FraudScore,
            ProcessorResponseCode = processorResult.ProcessorResponseCode,
            AuthCode = processorResult.AuthCode,
            ResponseMessage = processorResult.Approved
                ? "Payment approved."
                : $"Payment declined by processor. {processorResult.Message}",
            CardNetwork = fraudResult.CardNetwork,
            VelocityFlag = fraudResult.VelocityFlag,
            FraudChecks = fraudResult.ChecksPerformed
        };
    }
}
