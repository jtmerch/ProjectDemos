using ProcessorApi.DTOs;

namespace ProcessorApi.Services.IService;

// Contract for the service that handles payment authorization decisions
public interface IProcessorService
{
    // Validate the card details and return an approve or decline result
    Task<ProcessorResult> AuthorizeAsync(ProcessorAuthorizeRequest request, CancellationToken cancellationToken);
}
