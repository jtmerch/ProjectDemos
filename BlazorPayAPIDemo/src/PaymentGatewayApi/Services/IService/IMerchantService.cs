using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services.IService;

public interface IMerchantService
{
    Task<Merchant?> GetMerchantAsync(string merchantId, CancellationToken cancellationToken = default);
}
