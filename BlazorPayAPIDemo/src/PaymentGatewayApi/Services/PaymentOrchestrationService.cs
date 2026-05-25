using System.Diagnostics;
using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services.IService;

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

    public async Task<PaymentAuthorizationResponse> AuthorizeAsync(CardPaymentRequest request, string correlationId, CancellationToken cancellationToken = default)
    {
        // Merchant lookup
        _logger.LogInformation("[{CorrelationId}] Merchant lookup started merchantId={MerchantId}", correlationId, request.MerchantId);
        var merchant = await _merchantService.GetMerchantAsync(request.MerchantId, cancellationToken);

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

        // Parallel fraud + processor calls
        _logger.LogInformation("[{CorrelationId}] Fraud check and processor submission started in parallel", correlationId);

        var fraudSw = Stopwatch.StartNew();
        var processorSw = Stopwatch.StartNew();

        var fraudTask = _fraudClient.CheckFraudAsync(request, merchant, cancellationToken);
        var processorTask = _paymentProcessor.AuthorizeAsync(request, merchant, cancellationToken);

        await Task.WhenAll(fraudTask, processorTask);

        var fraudResult = await fraudTask;
        fraudSw.Stop();

        var processorResult = await processorTask;
        processorSw.Stop();

        _logger.LogInformation("[{CorrelationId}] Fraud check complete score={FraudScore} recommendation={Recommendation} elapsed={ElapsedMs}ms",
            correlationId, fraudResult.FraudScore, fraudResult.Recommendation, fraudSw.ElapsedMilliseconds);

        _logger.LogInformation("[{CorrelationId}] Processor response received code={ProcessorResponseCode} approved={Approved} transactionId={TransactionId} elapsed={ElapsedMs}ms",
            correlationId, processorResult.ProcessorResponseCode, processorResult.Approved, processorResult.TransactionId, processorSw.ElapsedMilliseconds);

        // Fraud gate
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
