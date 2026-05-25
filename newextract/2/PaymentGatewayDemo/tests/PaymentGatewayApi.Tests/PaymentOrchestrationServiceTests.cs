using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services;
using Xunit;

namespace PaymentGatewayApi.Tests;

public class PaymentOrchestrationServiceTests
{
    [Fact]
    public async Task AuthorizeAsync_Declines_WhenMerchantNotFound()
    {
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("BADID", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Merchant?)null);

        var service = CreateService(merchantService.Object);

        var result = await service.AuthorizeAsync(CreateRequest("BADID"));

        Assert.False(result.Approved);
        Assert.Equal("MERCHANT_NOT_FOUND", result.ProcessorResponseCode);
    }

    [Fact]
    public async Task AuthorizeAsync_Declines_WhenMerchantInactive()
    {
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M9999", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Merchant { MerchantId = "M9999", MerchantName = "Inactive", IsActive = false, ProcessorName = "SimulatedProcessor" });

        var service = CreateService(merchantService.Object);

        var result = await service.AuthorizeAsync(CreateRequest("M9999"));

        Assert.False(result.Approved);
        Assert.Equal("MERCHANT_INACTIVE", result.ProcessorResponseCode);
    }

    [Fact]
    public async Task AuthorizeAsync_Approves_WhenFraudAndProcessorApprove()
    {
        var merchant = new Merchant { MerchantId = "M1001", MerchantName = "Test Merchant", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Low" };
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M1001", It.IsAny<CancellationToken>())).ReturnsAsync(merchant);

        var fraudClient = new Mock<IFraudClient>();
        fraudClient.Setup(x => x.CheckFraudAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FraudResult { FraudScore = 10, Recommendation = "Approved", Message = "Low risk" });

        var processor = new Mock<PaymentProcessorBase>();
        processor.SetupGet(x => x.ProcessorName).Returns("SimulatedProcessor");
        processor.Setup(x => x.AuthorizeAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessorResult { Approved = true, ProcessorResponseCode = "00", AuthCode = "123456", Message = "Approved" });

        var service = new PaymentOrchestrationService(merchantService.Object, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);

        var result = await service.AuthorizeAsync(CreateRequest("M1001"));

        Assert.True(result.Approved);
        Assert.Equal("00", result.ProcessorResponseCode);
        Assert.Equal("123456", result.AuthCode);
    }

    [Fact]
    public async Task AuthorizeAsync_Declines_WhenFraudScoreHigh()
    {
        var merchant = new Merchant { MerchantId = "M1001", MerchantName = "Test Merchant", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "High" };
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M1001", It.IsAny<CancellationToken>())).ReturnsAsync(merchant);

        var fraudClient = new Mock<IFraudClient>();
        fraudClient.Setup(x => x.CheckFraudAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FraudResult { FraudScore = 90, Recommendation = "Declined", Message = "High risk" });

        var processor = new Mock<PaymentProcessorBase>();
        processor.Setup(x => x.AuthorizeAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessorResult { Approved = true, ProcessorResponseCode = "00", AuthCode = "123456", Message = "Approved" });

        var service = new PaymentOrchestrationService(merchantService.Object, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);

        var result = await service.AuthorizeAsync(CreateRequest("M1001"));

        Assert.False(result.Approved);
        Assert.Contains("fraud", result.ResponseMessage, StringComparison.OrdinalIgnoreCase);
    }

    private static PaymentOrchestrationService CreateService(IMerchantService merchantService)
    {
        var fraudClient = new Mock<IFraudClient>();
        var processor = new Mock<PaymentProcessorBase>();
        return new PaymentOrchestrationService(merchantService, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);
    }

    private static CardPaymentRequest CreateRequest(string merchantId) => new()
    {
        MerchantId = merchantId,
        Amount = 125.50m,
        Currency = "USD",
        CardNumber = "4111111111111111",
        ExpirationMonth = 12,
        ExpirationYear = 2030,
        CVV = "123",
        CustomerName = "Test Customer"
    };
}
