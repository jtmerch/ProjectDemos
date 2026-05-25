using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PaymentGatewayApi.Clients;
using PaymentGatewayApi.DTOs;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services;
using PaymentGatewayApi.Services.IService;
using Xunit;

namespace PaymentGatewayApi.Tests;

// Tests for PaymentOrchestrationService — covers the main payment flow scenarios
public class PaymentOrchestrationServiceTests
{
    // Should decline and return MERCHANT_NOT_FOUND when the merchant ID doesn't exist
    [Fact]
    public async Task AuthorizeAsync_Declines_WhenMerchantNotFound()
    {
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("BADID", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Merchant?)null);

        var service = CreateService(merchantService.Object);

        var result = await service.AuthorizeAsync(CreateRequest("BADID"), "test-corr");

        Assert.False(result.Approved);
        Assert.Equal("MERCHANT_NOT_FOUND", result.ProcessorResponseCode);
    }

    // Should decline and return MERCHANT_INACTIVE when the merchant account is disabled
    [Fact]
    public async Task AuthorizeAsync_Declines_WhenMerchantInactive()
    {
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M9999", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Merchant { MerchantId = "M9999", MerchantName = "Inactive", IsActive = false, ProcessorName = "SimulatedProcessor" });

        var service = CreateService(merchantService.Object);

        var result = await service.AuthorizeAsync(CreateRequest("M9999"), "test-corr");

        Assert.False(result.Approved);
        Assert.Equal("MERCHANT_INACTIVE", result.ProcessorResponseCode);
    }

    // Should approve when both fraud and processor return positive results
    [Fact]
    public async Task AuthorizeAsync_Approves_WhenFraudAndProcessorApprove()
    {
        var merchant = new Merchant { MerchantId = "M1001", MerchantName = "Test Merchant", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "Low" };
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M1001", It.IsAny<CancellationToken>())).ReturnsAsync(merchant);

        // Fraud engine says low risk
        var fraudClient = new Mock<IFraudClient>();
        fraudClient.Setup(x => x.CheckFraudAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FraudResult { FraudScore = 10, Recommendation = "Approved", Message = "Low risk" });

        // Processor says approved
        var processor = new Mock<PaymentProcessorBase>();
        processor.SetupGet(x => x.ProcessorName).Returns("SimulatedProcessor");
        processor.Setup(x => x.AuthorizeAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessorResult { Approved = true, ProcessorResponseCode = "00", AuthCode = "123456", Message = "Approved" });

        var service = new PaymentOrchestrationService(merchantService.Object, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);

        var result = await service.AuthorizeAsync(CreateRequest("M1001"), "test-corr");

        Assert.True(result.Approved);
        Assert.Equal("00", result.ProcessorResponseCode);
        Assert.Equal("123456", result.AuthCode);
    }

    // Should decline when the fraud score is high, even if the processor would have approved
    [Fact]
    public async Task AuthorizeAsync_Declines_WhenFraudScoreHigh()
    {
        var merchant = new Merchant { MerchantId = "M1001", MerchantName = "Test Merchant", IsActive = true, ProcessorName = "SimulatedProcessor", RiskLevel = "High" };
        var merchantService = new Mock<IMerchantService>();
        merchantService.Setup(x => x.GetMerchantAsync("M1001", It.IsAny<CancellationToken>())).ReturnsAsync(merchant);

        // Fraud engine says high risk — score of 90
        var fraudClient = new Mock<IFraudClient>();
        fraudClient.Setup(x => x.CheckFraudAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FraudResult { FraudScore = 90, Recommendation = "Declined", Message = "High risk" });

        // Processor would approve, but fraud overrides it
        var processor = new Mock<PaymentProcessorBase>();
        processor.Setup(x => x.AuthorizeAsync(It.IsAny<CardPaymentRequest>(), merchant, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessorResult { Approved = true, ProcessorResponseCode = "00", AuthCode = "123456", Message = "Approved" });

        var service = new PaymentOrchestrationService(merchantService.Object, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);

        var result = await service.AuthorizeAsync(CreateRequest("M1001"), "test-corr");

        Assert.False(result.Approved);
        Assert.Contains("fraud", result.ResponseMessage, StringComparison.OrdinalIgnoreCase);
    }

    // Shared helper — builds a service with default mocked fraud and processor dependencies
    private static PaymentOrchestrationService CreateService(IMerchantService merchantService)
    {
        var fraudClient = new Mock<IFraudClient>();
        var processor = new Mock<PaymentProcessorBase>();
        return new PaymentOrchestrationService(merchantService, fraudClient.Object, processor.Object, NullLogger<PaymentOrchestrationService>.Instance);
    }

    // Shared helper — builds a standard test payment request for a given merchant ID
    private static CardPaymentRequest CreateRequest(string merchantId)
    {
        return new CardPaymentRequest
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
}
