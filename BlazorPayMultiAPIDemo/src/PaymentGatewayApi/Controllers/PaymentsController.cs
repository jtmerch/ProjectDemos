using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services.IService;

namespace PaymentGatewayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentOrchestrationService _paymentService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentOrchestrationService paymentService, IMemoryCache cache, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _cache = cache;
        _logger = logger;
    }

    [HttpPost("authorize")]
    public async Task<ActionResult<PaymentAuthorizationResponse>> Authorize([FromBody] CardPaymentRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var correlationId = Guid.NewGuid().ToString();

        _logger.LogInformation("[{CorrelationId}] Request received merchant={MerchantId} amount={Amount} {Currency} idempotencyKey={IdempotencyKey}",
            correlationId, request.MerchantId, request.Amount, request.Currency, request.IdempotencyKey);

        if (_cache.TryGetValue(request.IdempotencyKey, out PaymentAuthorizationResponse? cached))
        {
            _logger.LogInformation("[{CorrelationId}] Duplicate request — returning cached response for idempotencyKey={IdempotencyKey}",
                correlationId, request.IdempotencyKey);
            return Ok(cached);
        }

        var response = await _paymentService.AuthorizeAsync(request, correlationId, cancellationToken);

        _cache.Set(request.IdempotencyKey, response, TimeSpan.FromHours(24));

        if (response.ProcessorResponseCode == "MERCHANT_NOT_FOUND")
            return NotFound(response);

        return Ok(response);
    }

    [HttpGet("status/{transactionId}")]
    public ActionResult<PaymentStatusResponse> GetStatus(string transactionId)
    {
        return Ok(new PaymentStatusResponse
        {
            TransactionId = transactionId,
            Status = "Approved"
        });
    }
}
