using PaymentGatewayApi.Repositories;
using PaymentGatewayApi.Services;
using Xunit;

namespace PaymentGatewayApi.Tests;

public class MerchantServiceTests
{
    [Fact]
    public async Task GetMerchantAsync_ReturnsMerchant_WhenMerchantExists()
    {
        var service = new MerchantService(new InMemoryMerchantRepository());

        var merchant = await service.GetMerchantAsync("M1001");

        Assert.NotNull(merchant);
        Assert.Equal("Savannah Urgent Care", merchant!.MerchantName);
    }

    [Fact]
    public async Task GetMerchantAsync_ReturnsNull_WhenMerchantDoesNotExist()
    {
        var service = new MerchantService(new InMemoryMerchantRepository());

        var merchant = await service.GetMerchantAsync("BADID");

        Assert.Null(merchant);
    }
}
