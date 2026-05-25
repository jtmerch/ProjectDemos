using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public interface IMerchantService
{
    Task<Merchant?> GetMerchantAsync(string merchantId, CancellationToken cancellationToken = default);
}
