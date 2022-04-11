namespace GasGuru.Api;

public interface IEntityRepo<T>
{
    IAsyncEnumerable<T> GetAllAsync(bool includeDeleted);
    Task<T?> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}
