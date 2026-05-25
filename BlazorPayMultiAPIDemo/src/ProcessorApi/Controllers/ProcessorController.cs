using Microsoft.AspNetCore.Mvc;
using ProcessorApi.DTOs;
using ProcessorApi.Services.IService;

namespace ProcessorApi.Controllers;

// This controller receives authorization requests from the Payment Gateway and returns approve/decline decisions
[ApiController]
[Route("api/[controller]")]
public class ProcessorController : ControllerBase
{
    private readonly IProcessorService _processorService;

    // Processor service is injected here to handle the actual evaluation logic
    public ProcessorController(IProcessorService processorService)
    {
        _processorService = processorService;
    }

    // POST api/processor/authorize — validates the card and decides whether to approve the transaction
    [HttpPost("authorize")]
    public async Task<ActionResult<ProcessorResult>> Authorize([FromBody] ProcessorAuthorizeRequest request, CancellationToken cancellationToken)
    {
        var result = await _processorService.AuthorizeAsync(request, cancellationToken);
        return Ok(result);
    }
}
