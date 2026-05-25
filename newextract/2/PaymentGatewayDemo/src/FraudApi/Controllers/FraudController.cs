using FraudApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FraudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FraudController : ControllerBase
{
    [HttpPost("check")]
    public async Task<ActionResult<FraudResult>> Check([FromBody] FraudCheckRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(3000, cancellationToken);

        var score = 15;
        var recommendation = "Approved";
        var message = "Low-risk transaction.";

        if (request.Amount > 5000 || request.RiskLevel.Equals("High", StringComparison.OrdinalIgnoreCase))
        {
            score = 90;
            recommendation = "Declined";
            message = "High-risk transaction based on amount or merchant risk level.";
        }
        else if (request.Amount > 1000 || request.RiskLevel.Equals("Medium", StringComparison.OrdinalIgnoreCase))
        {
            score = 55;
            recommendation = "Review";
            message = "Medium-risk transaction. Approved for demo purposes.";
        }

        return Ok(new FraudResult
        {
            FraudScore = score,
            Recommendation = recommendation,
            Message = message
        });
    }
}
