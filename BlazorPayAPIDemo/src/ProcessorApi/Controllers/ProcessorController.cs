using Microsoft.AspNetCore.Mvc;
using ProcessorApi.DTOs;
using ProcessorApi.Services.IService;

namespace ProcessorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessorController : ControllerBase
{
    private readonly IProcessorService _processorService;

    public ProcessorController(IProcessorService processorService)
    {
        _processorService = processorService;
    }

    [HttpPost("authorize")]
    public async Task<ActionResult<ProcessorResult>> Authorize([FromBody] ProcessorAuthorizeRequest request, CancellationToken cancellationToken)
    {
        var result = await _processorService.AuthorizeAsync(request, cancellationToken);
        return Ok(result);
    }
}
