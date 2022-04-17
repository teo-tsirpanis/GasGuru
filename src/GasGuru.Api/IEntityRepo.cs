namespace GasGuru.Api;

public interface IEntityRepo<TViewModel, TEditModel>
{
    // I could have made it return a sequence of GUIDs to avoid sending
    // too much data, but it would have been susceptible to N+1 queries.
    IAsyncEnumerable<TViewModel> GetAllAsync(bool includeDeleted);
    Task<TViewModel?> GetByIdAsync(Guid id);
    Task CreateAsync(TEditModel entity);
    Task UpdateAsync(Guid id, TEditModel entity);
    Task DeleteAsync(Guid id);
}
