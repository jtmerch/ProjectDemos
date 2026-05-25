using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Repositories;

public class InMemoryMerchantRepository : IMerchantRepository
{
    private readonly List<Merchant> _merchants =
    [
        new Merchant { MerchantId = "M1001", MerchantName = "Savannah Urgent Care", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Low" },
        new Merchant { MerchantId = "M1002", MerchantName = "Downtown Retail Shop", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Medium" },
        new Merchant { MerchantId = "M9999", MerchantName = "Inactive Merchant", IsActive = false, ProcessorName = "SimulatedProcessor", RiskLevel = "High" }
    ];

    public Task<Merchant?> GetByIdAsync(string merchantId, CancellationToken cancellationToken = default)
    {
        var merchant = _merchants.FirstOrDefault(m => m.MerchantId.Equals(merchantId, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(merchant);
    }
}
