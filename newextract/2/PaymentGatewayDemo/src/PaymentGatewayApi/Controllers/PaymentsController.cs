using Microsoft.AspNetCore.Mvc;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services;

namespace PaymentGatewayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentOrchestrationService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentOrchestrationService paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpPost("authorize")]
    public async Task<ActionResult<PaymentAuthorizationResponse>> Authorize([FromBody] CardPaymentRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Received payment authorization request for merchant {MerchantId}", request.MerchantId);

        var response = await _paymentService.AuthorizeAsync(request, cancellationToken);

        if (response.ProcessorResponseCode == "MERCHANT_NOT_FOUND")
        {
            return NotFound(response);
        }

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
