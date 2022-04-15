using GasGuru.Api;
using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class TransactionRepo : ITransactionRepo
{
    private readonly GasStationContext _context;

    public TransactionRepo(GasStationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TransactionCreateModel entity)
    {
        Transaction validated = await ValidateTransactionModelAsync(entity);
        await _context.Transactions.AddAsync(validated);
        await _context.SaveChangesAsync();
    }

    public IAsyncEnumerable<TransactionViewModel> GetAllAsync()
    {
        return _context.Transactions
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .Include(x => x.Lines)
            .ThenInclude(x => x.Item)
            .AsNoTracking()
            .Select(x => ConvertToViewModel(x))
            .AsAsyncEnumerable();
    }

    public async Task<TransactionViewModel?> GetByIdAsync(Guid id)
    {
        Transaction? transaction = await _context.Transactions
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .Include(x => x.Lines)
            .ThenInclude(x => x.Item)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (transaction is null)
            return null;
        return ConvertToViewModel(transaction);
    }

    private static TransactionViewModel ConvertToViewModel(Transaction transaction) =>
        new()
        {
            Id = transaction.Id,
            Date = transaction.Date,
            CustomerId = transaction.CustomerId,
            CustomerDisplay = $"{transaction.Customer.Name} {transaction.Customer.Surname}",
            EmployeeId = transaction.EmployeeId,
            EmployeeDisplay = $"{transaction.Employee.Name} {transaction.Employee.Surname}",
            PaymentMethod = (Api.PaymentMethod)transaction.PaymentMethod,
            Lines = transaction.Lines.Select(x => new TransactionLineViewModel()
            {
                ItemId = x.ItemId,
                ItemDisplay = $"{x.Item.Code}",
                Quantity = x.Quantity,
                DiscountPercent = x.DiscountPercent,
                ItemPrice = x.Item.Price
            }).ToList()
        };

    private async Task<TransactionLine> ValidateTransactionLineModelAsync(TransactionLineCreateModel model)
    {
        if (model.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");
        if (await _context.Items.FindAsync(model.ItemId) is not Item item)
            throw new InvalidOperationException("Item not found");

        decimal netPrice = item.Price * model.Quantity;
        decimal discountPercent = (item, netPrice) switch
        {
            ({ ItemType: Entities.ItemType.Fuel }, > 20) => 0.1m,
            _ => 0m
        };

        return new TransactionLine()
        {
            Item = item,
            Quantity = model.Quantity,
            DiscountPercent = discountPercent
        };
    }

    private async Task<Transaction> ValidateTransactionModelAsync(TransactionCreateModel model)
    {
        if (!Enum.IsDefined(model.PaymentMethod))
            throw new InvalidOperationException("Invalid payment method");
        if (model.Lines is { Count: 0 })
            throw new InvalidOperationException("Transaction must have at least one line");
        if (await _context.Customers.FindAsync(model.CustomerId) is not Customer customer)
            throw new InvalidOperationException("Customer not found");
        if (await _context.Employees.FindAsync(model.EmployeeId) is not Employee employee)
            throw new InvalidOperationException("Employee not found");

        var transaction = new Transaction(DateTime.Now, (Entities.PaymentMethod)model.PaymentMethod)
        {
            Customer = customer,
            Employee = employee
        };
        foreach (var line in model.Lines!)
        {
            transaction.Lines.Add(await ValidateTransactionLineModelAsync(line));
        }

        if (transaction.Lines.Count(x => x.Item.ItemType == Entities.ItemType.Fuel) > 1)
            throw new InvalidOperationException("At most one fuel item is allowed");
        switch (transaction.TotalValue, transaction.PaymentMethod)
        {
            case (> 50, not Entities.PaymentMethod.Cash):
                throw new InvalidOperationException($"Transactions above {50:C} are only allowed with cash");
            default:
                break;
        }

        return transaction;
    }
}
