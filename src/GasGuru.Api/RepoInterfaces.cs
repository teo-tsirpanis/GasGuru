namespace GasGuru.Api;

public interface ICustomerRepo : IEntityRepo<CustomerViewModel, CustomerEditModel>
{
    Task<CustomerViewModel?> GetByCardNumberAsync(string cardNumber);
}

public interface IEmployeeRepo : IEntityRepo<EmployeeViewModel, EmployeeEditModel> { }

public interface IItemRepo : IEntityRepo<ItemViewModel, ItemEditModel> { }
