using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services.IService;

namespace PaymentGatewayApi.Controllers;

// This controller is the main entry point for payment requests coming from the Blazor client
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentOrchestrationService _paymentService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<PaymentsController> _logger;

    // ASP.NET automatically injects these dependencies when the controller is created
    public PaymentsController(IPaymentOrchestrationService paymentService, IMemoryCache cache, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _cache = cache;
        _logger = logger;
    }

    // POST api/payments/authorize — this is where a payment request comes in and gets processed
    [HttpPost("authorize")]
    public async Task<ActionResult<PaymentAuthorizationResponse>> Authorize([FromBody] CardPaymentRequest request, CancellationToken cancellationToken)
    {
        // Make sure all required fields are present before we do anything
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Create a unique ID we can use to trace this request through all the logs
        var correlationId = Guid.NewGuid().ToString();

        _logger.LogInformation("[{CorrelationId}] Request received merchant={MerchantId} amount={Amount} {Currency} idempotencyKey={IdempotencyKey}",
            correlationId, request.MerchantId, request.Amount, request.Currency, request.IdempotencyKey);

        // If we've seen this idempotency key before, return the same response — no need to run it again
        if (_cache.TryGetValue(request.IdempotencyKey, out PaymentAuthorizationResponse? cached))
        {
            _logger.LogInformation("[{CorrelationId}] Duplicate request — returning cached response for idempotencyKey={IdempotencyKey}",
                correlationId, request.IdempotencyKey);
            return Ok(cached);
        }

        // Run the actual payment through fraud check and processor
        var response = await _paymentService.AuthorizeAsync(request, correlationId, cancellationToken);

        // Cache the result for 24 hours so duplicate requests don't reprocess the payment
        _cache.Set(request.IdempotencyKey, response, TimeSpan.FromHours(24));

        // Send back a 404 if the merchant ID wasn't recognized
        if (response.ProcessorResponseCode == "MERCHANT_NOT_FOUND")
            return NotFound(response);

        return Ok(response);
    }

    // GET api/payments/status/{transactionId} — simple endpoint to check the status of a transaction
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
