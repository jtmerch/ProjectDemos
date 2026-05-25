using PaymentGatewayApi.Models;
using PaymentGatewayApi.Repositories.IService;
using PaymentGatewayApi.Services.IService;

namespace PaymentGatewayApi.Services;

// This service handles merchant lookups — it sits between the orchestration layer and the data repository
public class MerchantService : IMerchantService
{
    private readonly IMerchantRepository _repository;

    // The repository is injected here so the service doesn't need to know where the data actually lives
    public MerchantService(IMerchantRepository repository)
    {
        _repository = repository;
    }

    // Look up a merchant by their ID — returns null if nothing is found
    public Task<Merchant?> GetMerchantAsync(string merchantId, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(merchantId, cancellationToken);
    }
}
