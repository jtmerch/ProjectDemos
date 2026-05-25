using System.Collections.Concurrent;
using ProcessorApi.Models;

namespace ProcessorApi.Repositories;

// In-memory store for transaction records — no database needed for this demo
public class InMemoryTransactionRepository : ITransactionRepository
{
    // ConcurrentBag is used here because multiple requests can come in at the same time
    private readonly ConcurrentBag<TransactionRecord> _transactions = new();

    // Add a new transaction record to the store
    public void Add(TransactionRecord record) => _transactions.Add(record);

    // Return all transactions sorted by most recent first
    public IReadOnlyList<TransactionRecord> GetAll() =>
        _transactions.OrderByDescending(t => t.Timestamp).ToList().AsReadOnly();

    // Find a specific transaction by its ID
    public TransactionRecord? GetById(string transactionId) =>
        _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
}
