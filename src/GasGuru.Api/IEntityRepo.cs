namespace GasGuru.Api;

public interface IEntityRepo<TViewModel, TEditModel>
{
    IAsyncEnumerable<TViewModel> GetAllAsync(bool includeDeleted);
    Task<TViewModel?> GetByIdAsync(Guid id);
    Task CreateAsync(TEditModel entity);
    Task UpdateAsync(Guid id, TEditModel entity);
    Task DeleteAsync(Guid id);
}
