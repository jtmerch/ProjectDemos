using FraudApi.DTOs;
using FraudApi.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace FraudApi.Controllers;

// This controller receives fraud check requests from the Payment Gateway and returns a risk score
[ApiController]
[Route("api/[controller]")]
public class FraudController : ControllerBase
{
    private readonly IFraudService _fraudService;

    // Fraud service is injected here to do the actual evaluation
    public FraudController(IFraudService fraudService)
    {
        _fraudService = fraudService;
    }

    // POST api/fraud/check — runs all our fraud rules against the transaction and returns a result
    [HttpPost("check")]
    public async Task<ActionResult<FraudResult>> Check([FromBody] FraudCheckRequest request, CancellationToken cancellationToken)
    {
        var result = await _fraudService.CheckAsync(request, cancellationToken);
        return Ok(result);
    }
}
