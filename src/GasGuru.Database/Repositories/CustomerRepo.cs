using GasGuru.Api;
using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class CustomerRepo : ICustomerRepo
{
    private readonly GasStationContext _context;

    public CustomerRepo(GasStationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CustomerEditModel entity)
    {
        Customer validated = ValidateEditModel(entity);
        await _context.Customers.AddAsync(validated);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Customer? customer = await _context.Customers.FindAsync(id);
        if (customer is not null)
        {
            customer.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public IAsyncEnumerable<CustomerViewModel> GetAllAsync(bool includeDeleted)
    {
        IQueryable<Customer> query = _context.Customers;
        if (!includeDeleted)
            query = query.Where(c => !c.IsDeleted);
        return query.AsNoTracking().Select(x => ConvertToViewModel(x)).AsAsyncEnumerable();
    }

    public async Task<CustomerViewModel?> GetByIdAsync(Guid id)
    {
        Customer? customer = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (customer is null)
            return null;
        return ConvertToViewModel(customer);
    }

    public async Task<CustomerViewModel?> GetByCardNumberAsync(string cardNumber)
    {
        Customer? customer = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

        if (customer is null)
            return null;
        return ConvertToViewModel(customer);
    }

    public async Task UpdateAsync(Guid id, CustomerEditModel entity)
    {
        var validated = ValidateEditModel(entity);
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null)
            await _context.Customers.AddAsync(validated);
        else
            UpdateCustomer(customer, validated);
        await _context.SaveChangesAsync();
    }

    private static CustomerViewModel ConvertToViewModel(Customer customer) =>
        new()
        {
            Id = customer.Id,
            Name = customer.Name,
            Surname = customer.Surname,
            CardNumber = customer.CardNumber,
            IsDeleted = customer.IsDeleted
        };

    private static void UpdateCustomer(Customer destination, Customer source)
    {
        destination.Name = source.Name;
        destination.Surname = source.Surname;
        destination.CardNumber = source.CardNumber;
        destination.IsDeleted = source.IsDeleted;
    }

    private static Customer ValidateEditModel(CustomerEditModel model)
    {
        if (model.Name is null)
            throw new InvalidOperationException("Customer name is required");
        if (model.Surname is null)
            throw new InvalidOperationException("Customer surname is required");
        if (model.CardNumber is null || !model.CardNumber.StartsWith('A'))
            throw new InvalidOperationException("Card number must start with A");

        return new Customer(model.Name, model.Surname, model.CardNumber);
    }
}
