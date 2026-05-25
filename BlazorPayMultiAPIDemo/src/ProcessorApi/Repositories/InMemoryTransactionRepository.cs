using System.Collections.Concurrent;
using ProcessorApi.Models;

namespace ProcessorApi.Repositories;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly ConcurrentBag<TransactionRecord> _transactions = new();

    public void Add(TransactionRecord record) => _transactions.Add(record);

    public IReadOnlyList<TransactionRecord> GetAll() =>
        _transactions.OrderByDescending(t => t.Timestamp).ToList().AsReadOnly();

    public TransactionRecord? GetById(string transactionId) =>
        _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
}
