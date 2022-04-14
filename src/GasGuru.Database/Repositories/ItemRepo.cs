using GasGuru.Api;
using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class ItemRepo : IEntityRepo<ItemModel>
{
    private readonly GasStationContext _context;

    public ItemRepo(GasStationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(ItemModel entity)
    {
        Item validated = ValidateItemModel(entity);
        await _context.Items.AddAsync(validated);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Item? item = await _context.Items.FindAsync(id);
        if (item is not null)
        {
            item.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public IAsyncEnumerable<ItemModel> GetAllAsync(bool includeDeleted)
    {
        IQueryable<Item> query = _context.Items.AsNoTracking();
        if (!includeDeleted)
            query = query.Where(c => !c.IsDeleted);
        return query.Select(x => ConvertToItemModel(x)).AsAsyncEnumerable();
    }

    public async Task<ItemModel?> GetByIdAsync(Guid id)
    {
        Item? item = await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item is null)
            return null;
        return ConvertToItemModel(item);
    }

    public async Task UpdateAsync(Guid id, ItemModel entity)
    {
        var validated = ValidateItemModel(entity);
        var item = await _context.Items.FindAsync(id);
        if (item is null)
            await _context.Items.AddAsync(validated);
        else
            UpdateItem(item, validated);
        await _context.SaveChangesAsync();
    }

    private static ItemModel ConvertToItemModel(Item item) =>
        new()
        {
            Id = item.Id,
            Code = item.Code,
            Description = item.Description,
            ItemType = (Api.ItemType)item.ItemType,
            Price = item.Price,
            Cost = item.Cost,
            IsDeleted = item.IsDeleted
        };

    private static void UpdateItem(Item destination, Item source)
    {
        destination.Code = source.Code;
        destination.Description = source.Description;
        destination.ItemType = source.ItemType;
        destination.Price = source.Price;
        destination.Cost = source.Cost;
        destination.IsDeleted = source.IsDeleted;
    }

    private static Item ValidateItemModel(ItemModel model)
    {
        if (string.IsNullOrEmpty(model.Code))
            throw new InvalidOperationException("Item code is required");
        if (string.IsNullOrEmpty(model.Description))
            throw new InvalidOperationException("Item description is required");
        if (model.Price <= 0)
            throw new InvalidOperationException("Item price must be greater than 0");
        if (model.Cost <= 0)
            throw new InvalidOperationException("Item cost must be greater than 0");

        return new Item(model.Code, model.Description, (Entities.ItemType)model.ItemType, model.Price, model.Cost);
    }
}
