using FraudApi.DTOs;
using FraudApi.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace FraudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FraudController : ControllerBase
{
    private readonly IFraudService _fraudService;

    public FraudController(IFraudService fraudService)
    {
        _fraudService = fraudService;
    }

    [HttpPost("check")]
    public async Task<ActionResult<FraudResult>> Check([FromBody] FraudCheckRequest request, CancellationToken cancellationToken)
    {
        var result = await _fraudService.CheckAsync(request, cancellationToken);
        return Ok(result);
    }
}
