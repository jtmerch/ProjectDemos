using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services.IService;

// Contract for looking up merchant records
public interface IMerchantService
{
    // Returns the merchant if found, or null if the ID doesn't exist
    Task<Merchant?> GetMerchantAsync(string merchantId, CancellationToken cancellationToken = default);
}
