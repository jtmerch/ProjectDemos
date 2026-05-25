using System.Collections.Concurrent;
using FraudApi.DTOs;
using FraudApi.Services.IService;

namespace FraudApi.Services;

public class FraudService : IFraudService
{
    private static readonly HashSet<string> _blacklistedCards = new()
    {
        "4111111111110001",
        "4111111111110002",
        "5500000000000004"
    };

    private static readonly ConcurrentDictionary<string, List<DateTime>> _velocityTracker = new();
    private static readonly object _velocityLock = new();

    public async Task<FraudResult> CheckAsync(FraudCheckRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(3000, cancellationToken);

        var checks = new List<string>();
        var score = 0;

        var network = DetectCardNetwork(request.CardNumber);
        checks.Add($"BIN lookup: {network}");

        var isBlacklisted = IsBlacklisted(request.CardNumber);
        checks.Add($"Blacklist check: {(isBlacklisted ? "FLAGGED" : "Clear")}");

        if (isBlacklisted)
        {
            return new FraudResult
            {
                FraudScore = 100,
                Recommendation = "Declined",
                Message = "Card is on the fraud blacklist.",
                CardNetwork = network,
                VelocityFlag = false,
                ChecksPerformed = checks
            };
        }

        var highVelocity = CheckVelocity(request.CardNumber);
        checks.Add($"Velocity check: {(highVelocity ? "HIGH — 3+ transactions in 60s" : "Normal")}");
        if (highVelocity) score += 30;

        if (request.Amount > 5000 || request.RiskLevel.Equals("High", StringComparison.OrdinalIgnoreCase))
        {
            score += 50;
            checks.Add("Amount/risk check: High (+50)");
        }
        else if (request.Amount > 1000 || request.RiskLevel.Equals("Medium", StringComparison.OrdinalIgnoreCase))
        {
            score += 20;
            checks.Add("Amount/risk check: Medium (+20)");
        }
        else
        {
            checks.Add("Amount/risk check: Low (+0)");
        }

        if (network == "Unknown")
        {
            score += 15;
            checks.Add("Unknown card network: +15 risk points");
        }

        string recommendation;
        string message;

        switch (score)
        {
            case int s when s >= 80:
                recommendation = "Declined";
                message = "High-risk transaction declined by fraud engine.";
                break;
            case int s when s >= 50:
                recommendation = "Review";
                message = "Medium-risk transaction. Approved for demo purposes.";
                break;
            default:
                recommendation = "Approved";
                message = "Low-risk transaction.";
                break;
        }

        return new FraudResult
        {
            FraudScore = score,
            Recommendation = recommendation,
            Message = message,
            CardNetwork = network,
            VelocityFlag = highVelocity,
            ChecksPerformed = checks
        };
    }

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

    private static bool IsBlacklisted(string cardNumber) =>
        _blacklistedCards.Contains(cardNumber);

    private static bool CheckVelocity(string cardNumber)
    {
        lock (_velocityLock)
        {
            var now = DateTime.UtcNow;
            var cutoff = now.AddSeconds(-60);

            if (!_velocityTracker.TryGetValue(cardNumber, out var timestamps))
            {
                timestamps = new List<DateTime>();
                _velocityTracker[cardNumber] = timestamps;
            }

            timestamps.RemoveAll(t => t < cutoff);
            timestamps.Add(now);

            return timestamps.Count >= 3;
        }
    }
}
