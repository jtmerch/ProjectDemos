using ProcessorApi.DTOs;
using ProcessorApi.Models;
using ProcessorApi.Repositories;
using ProcessorApi.Services.IService;

namespace ProcessorApi.Services;

public class ProcessorService : IProcessorService
{
    private readonly ITransactionRepository _transactionRepository;

    public ProcessorService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ProcessorResult> AuthorizeAsync(ProcessorAuthorizeRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);

        var transactionId = Guid.NewGuid().ToString("N");
        var network = DetectCardNetwork(request.CardNumber);

        var result = Evaluate(transactionId, network, request);

        _transactionRepository.Add(new TransactionRecord
        {
            TransactionId = transactionId,
            MerchantId = request.MerchantId,
            Amount = request.Amount,
            Currency = request.Currency,
            CardNumberMasked = MaskCard(request.CardNumber),
            CardNetwork = network,
            Approved = result.Approved,
            ResponseCode = result.ProcessorResponseCode,
            AuthCode = result.AuthCode,
            Timestamp = DateTime.UtcNow
        });

        return result;
    }

    private static ProcessorResult Evaluate(string transactionId, string network, ProcessorAuthorizeRequest request)
    {
        if (request.Amount <= 0)
            return Decline(transactionId, network, "13", "Invalid amount.");

        if (IsExpired(request.ExpirationYear, request.ExpirationMonth))
            return Decline(transactionId, network, "54", "Expired card.");

        if (!IsCvvValid(request.CVV, network))
            return Decline(transactionId, network, "82", "Invalid CVV length for card type.");

        if (network == "Unknown")
            return Decline(transactionId, network, "14", "Invalid card — unrecognized network.");

        if (request.Amount > 10000)
            return Decline(transactionId, network, "51", "Insufficient funds.");

        if (request.CardNumber.EndsWith("0000"))
            return Decline(transactionId, network, "05", "Do not honor.");

        return new ProcessorResult
        {
            TransactionId = transactionId,
            Approved = true,
            ProcessorResponseCode = "00",
            AuthCode = Random.Shared.Next(100000, 999999).ToString(),
            Message = $"Approved via {network} network.",
            CardNetwork = network
        };
    }

    private static ProcessorResult Decline(string transactionId, string network, string code, string message) => new()
    {
        TransactionId = transactionId,
        Approved = false,
        ProcessorResponseCode = code,
        Message = message,
        CardNetwork = network
    };

    private static string DetectCardNetwork(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
            return "Unknown";

        if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
            return "American Express";

        if (cardNumber.StartsWith("4"))
            return "Visa";

        if (cardNumber.Length >= 2 && int.TryParse(cardNumber[..2], out var prefix2) && prefix2 >= 51 && prefix2 <= 55)
            return "Mastercard";

        if (cardNumber.StartsWith("6011") || cardNumber.StartsWith("65"))
            return "Discover";

        return "Unknown";
    }

    private static bool IsExpired(int year, int month)
    {
        var now = DateTime.UtcNow;
        return year < now.Year || (year == now.Year && month < now.Month);
    }

    private static bool IsCvvValid(string cvv, string network)
    {
        var expected = network == "American Express" ? 4 : 3;
        return cvv.Length == expected && cvv.All(char.IsDigit);
    }

    private static string MaskCard(string cardNumber) =>
        cardNumber.Length >= 4 ? $"****{cardNumber[^4..]}" : "****";
}
