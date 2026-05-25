using FraudApi.DTOs;

namespace FraudApi.Services.IService;

public interface IFraudService
{
    Task<FraudResult> CheckAsync(FraudCheckRequest request, CancellationToken cancellationToken);
}
