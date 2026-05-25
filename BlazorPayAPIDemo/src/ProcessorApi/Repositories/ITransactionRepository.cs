using ProcessorApi.Models;

namespace ProcessorApi.Repositories;

public interface ITransactionRepository
{
    void Add(TransactionRecord record);
    IReadOnlyList<TransactionRecord> GetAll();
    TransactionRecord? GetById(string transactionId);
}
