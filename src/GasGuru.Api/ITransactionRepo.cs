namespace GasGuru.Api;

public interface ITransactionRepo
{
    IAsyncEnumerable<TransactionViewModel> GetAllAsync();
    Task<TransactionViewModel?> GetByIdAsync(Guid id);
    Task CreateAsync(TransactionCreateModel transaction);
}
