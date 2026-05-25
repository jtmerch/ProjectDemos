using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Repositories.IService;

// Contract for however we store and retrieve merchant data (in-memory, database, etc.)
public interface IMerchantRepository
{
    // Look up a merchant by their ID — returns null if not found
    Task<Merchant?> GetByIdAsync(string merchantId, CancellationToken cancellationToken = default);
}
