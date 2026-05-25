using PaymentGatewayApi.Models;
using PaymentGatewayApi.Repositories.IService;

namespace PaymentGatewayApi.Repositories;

// In-memory stand-in for a real database — stores a small list of merchants for demo purposes
public class InMemoryMerchantRepository : IMerchantRepository
{
    // Hardcoded merchant list — M9999 is intentionally inactive so we can test that scenario
    private readonly List<Merchant> _merchants =
    [
        new Merchant { MerchantId = "M1001", MerchantName = "Savannah Urgent Care", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Low" },
        new Merchant { MerchantId = "M1002", MerchantName = "Downtown Retail Shop", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Medium" },
        new Merchant { MerchantId = "M9999", MerchantName = "Inactive Merchant", IsActive = false, ProcessorName = "SimulatedProcessor", RiskLevel = "High" }
    ];

    // Find a merchant by ID — not case sensitive so M1001 and m1001 both work
    public Task<Merchant?> GetByIdAsync(string merchantId, CancellationToken cancellationToken = default)
    {
        var merchant = _merchants.FirstOrDefault(m => m.MerchantId.Equals(merchantId, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(merchant);
    }
}
