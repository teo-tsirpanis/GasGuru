namespace GasGuru.Api;

public interface IMonthlyLedgerRepo
{
    Task<MonthlyLedger> GetMonthlyLedgerAsync(int year, int month);
}
