using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Repositories.IService;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(string merchantId, CancellationToken cancellationToken = default);
}
