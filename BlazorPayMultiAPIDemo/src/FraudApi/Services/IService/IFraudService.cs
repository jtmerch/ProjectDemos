using FraudApi.DTOs;

namespace FraudApi.Services.IService;

// Contract for the fraud evaluation service
public interface IFraudService
{
    // Run the transaction through all fraud rules and return a score and recommendation
    Task<FraudResult> CheckAsync(FraudCheckRequest request, CancellationToken cancellationToken);
}
