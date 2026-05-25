using ProcessorApi.Models;

namespace ProcessorApi.Repositories;

// Contract for storing and retrieving transaction records
public interface ITransactionRepository
{
    // Save a new transaction record
    void Add(TransactionRecord record);

    // Get all transactions, sorted newest first
    IReadOnlyList<TransactionRecord> GetAll();

    // Look up a single transaction by its ID
    TransactionRecord? GetById(string transactionId);
}
