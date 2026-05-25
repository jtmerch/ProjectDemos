using System.Collections.Concurrent;
using FraudApi.DTOs;
using FraudApi.Services.IService;

namespace FraudApi.Services;

// This service runs all of our fraud detection logic and produces a risk score for each transaction
public class FraudService : IFraudService
{
    // Cards that are always blocked no matter what — known fraud cards for testing
    private static readonly HashSet<string> _blacklistedCards = new()
    {
        "4111111111110001",
        "4111111111110002",
        "5500000000000004"
    };

    // Tracks how many times each card number has been used recently — used for velocity checks
    private static readonly ConcurrentDictionary<string, List<DateTime>> _velocityTracker = new();
    private static readonly object _velocityLock = new();

    public async Task<FraudResult> CheckAsync(FraudCheckRequest request, CancellationToken cancellationToken)
    {
        // Simulate real-world processing time for the fraud engine
        await Task.Delay(3000, cancellationToken);

        // Keep a running list of what checks we ran so we can include them in the response
        var checks = new List<string>();
        var score = 0;

        // Check 1 — figure out what card network this is (Visa, Mastercard, etc.)
        var network = DetectCardNetwork(request.CardNumber);
        checks.Add($"BIN lookup: {network}");

        // Check 2 — see if this card is on our blacklist
        var isBlacklisted = IsBlacklisted(request.CardNumber);
        checks.Add($"Blacklist check: {(isBlacklisted ? "FLAGGED" : "Clear")}");

        // If the card is blacklisted, stop here and decline immediately — no need to check anything else
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

        // Check 3 — see if this card has been used 3 or more times in the last 60 seconds
        var highVelocity = CheckVelocity(request.CardNumber);
        checks.Add($"Velocity check: {(highVelocity ? "HIGH — 3+ transactions in 60s" : "Normal")}");
        if (highVelocity) score += 30;

        // Check 4 — score based on transaction amount and merchant risk tier
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

        // Check 5 — if we can't identify the card network, add some extra risk points
        if (network == "Unknown")
        {
            score += 15;
            checks.Add("Unknown card network: +15 risk points");
        }

        // Decide the final recommendation based on the total score
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

    // Determine the card network based on the first few digits of the card number
    private static string DetectCardNetwork(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
            return "Unknown";

        // Amex cards start with 34 or 37
        if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
            return "American Express";

        // Visa cards start with 4
        if (cardNumber.StartsWith("4"))
            return "Visa";

        // Mastercard cards start with 51–55
        if (cardNumber.Length >= 2 && int.TryParse(cardNumber[..2], out var prefix2) && prefix2 >= 51 && prefix2 <= 55)
            return "Mastercard";

        // Discover cards start with 6011 or 65
        if (cardNumber.StartsWith("6011") || cardNumber.StartsWith("65"))
            return "Discover";

        return "Unknown";
    }

    // Check if the card number is in our blocked list
    private static bool IsBlacklisted(string cardNumber) =>
        _blacklistedCards.Contains(cardNumber);

    // Track how many times this card has been used in the last 60 seconds
    private static bool CheckVelocity(string cardNumber)
    {
        lock (_velocityLock)
        {
            var now = DateTime.UtcNow;
            var cutoff = now.AddSeconds(-60);

            // Get or create the list of timestamps for this card
            if (!_velocityTracker.TryGetValue(cardNumber, out var timestamps))
            {
                timestamps = new List<DateTime>();
                _velocityTracker[cardNumber] = timestamps;
            }

            // Remove timestamps older than 60 seconds, then add the current one
            timestamps.RemoveAll(t => t < cutoff);
            timestamps.Add(now);

            // Flag as high velocity if 3 or more uses in the window
            return timestamps.Count >= 3;
        }
    }
}
