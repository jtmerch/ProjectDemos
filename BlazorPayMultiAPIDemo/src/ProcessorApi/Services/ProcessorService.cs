using ProcessorApi.DTOs;
using ProcessorApi.Models;
using ProcessorApi.Repositories;
using ProcessorApi.Services.IService;

namespace ProcessorApi.Services;

// This service simulates a payment processor — it validates the card and decides approve or decline
public class ProcessorService : IProcessorService
{
    private readonly ITransactionRepository _transactionRepository;

    // Repository is injected so we can save a record of every transaction that comes through
    public ProcessorService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ProcessorResult> AuthorizeAsync(ProcessorAuthorizeRequest request, CancellationToken cancellationToken)
    {
        // Simulate real-world processing time for a payment authorization
        await Task.Delay(5000, cancellationToken);

        // Generate a unique ID for this transaction and detect the card network
        var transactionId = Guid.NewGuid().ToString("N");
        var network = DetectCardNetwork(request.CardNumber);

        // Run through all our validation rules and get the result
        var result = Evaluate(transactionId, network, request);

        // Save a record of this transaction regardless of whether it was approved or declined
        _transactionRepository.Add(new TransactionRecord
        {
            TransactionId = transactionId,
            MerchantId = request.MerchantId,
            Amount = request.Amount,
            Currency = request.Currency,
            CardNumberMasked = MaskCard(request.CardNumber),  // Never store the full card number
            CardNetwork = network,
            Approved = result.Approved,
            ResponseCode = result.ProcessorResponseCode,
            AuthCode = result.AuthCode,
            Timestamp = DateTime.UtcNow
        });

        return result;
    }

    // Run through all the validation checks and return an approval or decline
    private static ProcessorResult Evaluate(string transactionId, string network, ProcessorAuthorizeRequest request)
    {
        // Can't charge a zero or negative amount
        if (request.Amount <= 0)
            return Decline(transactionId, network, "13", "Invalid amount.");

        // Check if the card has already expired
        if (IsExpired(request.ExpirationYear, request.ExpirationMonth))
            return Decline(transactionId, network, "54", "Expired card.");

        // CVV length must match what's expected for this card type (Amex uses 4 digits, others use 3)
        if (!IsCvvValid(request.CVV, network))
            return Decline(transactionId, network, "82", "Invalid CVV length for card type.");

        // We can't process cards from an unrecognized network
        if (network == "Unknown")
            return Decline(transactionId, network, "14", "Invalid card — unrecognized network.");

        // Decline anything over $10,000 — simulates an insufficient funds scenario
        if (request.Amount > 10000)
            return Decline(transactionId, network, "51", "Insufficient funds.");

        // Cards ending in 0000 are always declined — useful for testing decline scenarios
        if (request.CardNumber.EndsWith("0000"))
            return Decline(transactionId, network, "05", "Do not honor.");

        // All checks passed — approve the transaction and generate an auth code
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

    // Helper that builds a declined result — avoids repeating the same object setup everywhere
    private static ProcessorResult Decline(string transactionId, string network, string code, string message) => new()
    {
        TransactionId = transactionId,
        Approved = false,
        ProcessorResponseCode = code,
        Message = message,
        CardNetwork = network
    };

    // Determine the card network from the card number's first few digits
    private static string DetectCardNetwork(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
            return "Unknown";

        // Amex starts with 34 or 37
        if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
            return "American Express";

        // Visa starts with 4
        if (cardNumber.StartsWith("4"))
            return "Visa";

        // Mastercard is 51–55
        if (cardNumber.Length >= 2 && int.TryParse(cardNumber[..2], out var prefix2) && prefix2 >= 51 && prefix2 <= 55)
            return "Mastercard";

        // Discover starts with 6011 or 65
        if (cardNumber.StartsWith("6011") || cardNumber.StartsWith("65"))
            return "Discover";

        return "Unknown";
    }

    // Check if the card's expiration date has already passed
    private static bool IsExpired(int year, int month)
    {
        var now = DateTime.UtcNow;
        return year < now.Year || (year == now.Year && month < now.Month);
    }

    // Validate CVV length — Amex requires 4 digits, all other networks require 3
    private static bool IsCvvValid(string cvv, string network)
    {
        var expected = network == "American Express" ? 4 : 3;
        return cvv.Length == expected && cvv.All(char.IsDigit);
    }

    // Mask the card number so only the last 4 digits are visible — for safe storage in logs and records
    private static string MaskCard(string cardNumber) =>
        cardNumber.Length >= 4 ? $"****{cardNumber[^4..]}" : "****";
}
