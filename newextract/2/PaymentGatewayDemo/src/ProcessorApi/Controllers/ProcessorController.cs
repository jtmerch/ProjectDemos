using Microsoft.AspNetCore.Mvc;
using ProcessorApi.DTOs;

namespace ProcessorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessorController : ControllerBase
{
    [HttpPost("authorize")]
    public async Task<ActionResult<ProcessorResult>> Authorize([FromBody] ProcessorAuthorizeRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);

        if (request.Amount <= 0)
        {
            return Ok(new ProcessorResult { Approved = false, ProcessorResponseCode = "13", Message = "Invalid amount." });
        }

        if (request.CardNumber.EndsWith("0000"))
        {
            return Ok(new ProcessorResult { Approved = false, ProcessorResponseCode = "05", Message = "Do not honor." });
        }

        return Ok(new ProcessorResult
        {
            Approved = true,
            ProcessorResponseCode = "00",
            AuthCode = Random.Shared.Next(100000, 999999).ToString(),
            Message = "Approved by simulated processor."
        });
    }
}
