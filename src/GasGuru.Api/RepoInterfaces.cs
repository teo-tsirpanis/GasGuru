namespace GasGuru.Api;

public interface ICustomerRepo : IEntityRepo<CustomerViewModel, CustomerEditModel> { }

public interface IEmployeeRepo : IEntityRepo<EmployeeViewModel, EmployeeEditModel> { }

public interface IItemRepo : IEntityRepo<ItemViewModel, ItemEditModel> { }
