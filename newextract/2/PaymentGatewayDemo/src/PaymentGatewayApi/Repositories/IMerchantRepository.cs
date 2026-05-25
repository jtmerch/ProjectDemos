using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Repositories;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(string merchantId, CancellationToken cancellationToken = default);
}
