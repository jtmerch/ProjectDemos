using PaymentGatewayApi.Models;
using PaymentGatewayApi.Repositories;

namespace PaymentGatewayApi.Services;

public class MerchantService : IMerchantService
{
    private readonly IMerchantRepository _repository;

    public MerchantService(IMerchantRepository repository)
    {
        _repository = repository;
    }

    public Task<Merchant?> GetMerchantAsync(string merchantId, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(merchantId, cancellationToken);
    }
}
