using ProcessorApi.DTOs;

namespace ProcessorApi.Services.IService;

public interface IProcessorService
{
    Task<ProcessorResult> AuthorizeAsync(ProcessorAuthorizeRequest request, CancellationToken cancellationToken);
}
