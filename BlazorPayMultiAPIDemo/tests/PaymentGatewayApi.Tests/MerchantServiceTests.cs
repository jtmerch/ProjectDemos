using PaymentGatewayApi.Repositories;
using PaymentGatewayApi.Services;
using Xunit;

namespace PaymentGatewayApi.Tests;

// Tests for MerchantService — verifies that merchant lookups work correctly
public class MerchantServiceTests
{
    // Should return the merchant when a valid ID is passed in
    [Fact]
    public async Task GetMerchantAsync_ReturnsMerchant_WhenMerchantExists()
    {
        var service = new MerchantService(new InMemoryMerchantRepository());

        var merchant = await service.GetMerchantAsync("M1001");

        Assert.NotNull(merchant);
        Assert.Equal("Savannah Urgent Care", merchant!.MerchantName);
    }

    // Should return null instead of throwing when the merchant ID doesn't exist
    [Fact]
    public async Task GetMerchantAsync_ReturnsNull_WhenMerchantDoesNotExist()
    {
        var service = new MerchantService(new InMemoryMerchantRepository());

        var merchant = await service.GetMerchantAsync("BADID");

        Assert.Null(merchant);
    }
}
