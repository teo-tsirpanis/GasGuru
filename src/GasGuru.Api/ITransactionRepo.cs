namespace GasGuru.Api;

public interface ITransactionRepo
{
    IAsyncEnumerable<TransactionModel> GetAllAsync();
    Task<TransactionModel?> GetByIdAsync(Guid id);
    Task CreateAsync(TransactionModel transaction);
}
