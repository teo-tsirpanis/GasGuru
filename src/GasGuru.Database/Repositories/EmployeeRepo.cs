using GasGuru.Api;
using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class EmployeeRepo : IEmployeeRepo
{
    private readonly GasStationContext _context;

    public EmployeeRepo(GasStationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(EmployeeEditModel entity)
    {
        Employee validated = ValidateEditModel(entity);
        await _context.Employees.AddAsync(validated);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Employee? employee = await _context.Employees.FindAsync(id);
        if (employee is not null)
        {
            employee.HireDateEnd = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

    public IAsyncEnumerable<EmployeeViewModel> GetAllAsync(bool includeDeleted)
    {
        IQueryable<Employee> query = _context.Employees.AsNoTracking();
        if (!includeDeleted)
            query = query.Where(x => x.HireDateEnd.HasValue);
        return query.Select(x => ConvertToViewModel(x)).AsAsyncEnumerable();
    }

    public async Task<EmployeeViewModel?> GetByIdAsync(Guid id)
    {
        Employee? employee = await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (employee is null)
            return null;
        return ConvertToViewModel(employee);
    }

    public async Task UpdateAsync(Guid id, EmployeeEditModel entity)
    {
        var validated = ValidateEditModel(entity);
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null)
            await _context.Employees.AddAsync(validated);
        else
            UpdateEmployee(employee, validated);
        await _context.SaveChangesAsync();
    }

    private static EmployeeViewModel ConvertToViewModel(Employee employee) =>
        new()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            HireDateStart = employee.HireDateStart,
            HireDateEnd = employee.HireDateEnd,
            SalaryPerMonth = employee.SalaryPerMonth,
            EmployeeType = (Api.EmployeeType)employee.EmployeeType,
        };

    private static void UpdateEmployee(Employee destination, Employee source)
    {
        destination.Name = source.Name;
        destination.Surname = source.Surname;
        destination.HireDateStart = source.HireDateStart;
        destination.HireDateEnd = source.HireDateEnd;
        destination.SalaryPerMonth = source.SalaryPerMonth;
        destination.EmployeeType = source.EmployeeType;
    }

    private static Employee ValidateEditModel(EmployeeEditModel model)
    {
        if (model.Name is null)
            throw new InvalidOperationException("Employee name is required");
        if (model.Surname is null)
            throw new InvalidOperationException("Employee surname is required");
        if (!Enum.IsDefined(model.EmployeeType))
            throw new InvalidOperationException("Invalid employee type");
        if (model.SalaryPerMonth <= 0)
            throw new InvalidOperationException("Salary per month must be positive");

        var employeeType = (Entities.EmployeeType)model.EmployeeType;
        return new Employee(model.Name, model.Surname, model.SalaryPerMonth, employeeType);
    }
}
