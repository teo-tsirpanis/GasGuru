using GasGuru.Api;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class MonthlyLedgerRepo : IMonthlyLedgerRepo
{
    private readonly GasStationContext _context;

    public MonthlyLedgerRepo(GasStationContext context)
    {
        _context = context;
    }
    
    public async Task<MonthlyLedger> GetMonthlyLedgerAsync(int year, int month)
    {
        if (year < 0 || month < 0 || month > 12)
        {
            throw new InvalidOperationException("Invalid year or month");
        }

        decimal rent = (await _context.GasStationOptions.AsNoTracking().SingleAsync()).MonthlyRent;
        decimal sales = await _context.Transactions
            .Where(s => s.Date.Year == year && s.Date.Month == month)
            .Include(x => x.Lines)
            .SelectMany(x => x.Lines)
            .SumAsync(s => s.TotalPrice);
        var salaries = await _context.Employees
            .Where(x => x.HireDateStart >= new DateTime(year, month, 1) && x.HireDateEnd <= new DateTime(year, month, 1).AddMonths(1))
            .SumAsync(x => x.SalaryPerMonth);
        return new() { Expenses = rent + salaries, Income = sales };
    }
}
