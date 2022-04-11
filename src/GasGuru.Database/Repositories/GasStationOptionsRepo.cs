using GasGuru.Api;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database.Repositories;

internal class GasStationOptionsRepo : IGasStationOptionsRepo
{
    private readonly GasStationContext _context;

    public GasStationOptionsRepo(GasStationContext context)
    {
        _context = context;
    }

    public async Task<GasStationOptionsModel> GetOptionsAsync()
    {
        var options = await _context.GasStationOptions.AsNoTracking().SingleAsync();
        return new() { MonthlyRent = options.MonthlyRent };
    }

    public async Task UpdateOptionsAsync(GasStationOptionsModel options)
    {
        if (options.MonthlyRent < 0)
        {
            throw new ArgumentException("Monthly rent cannot be negative");
        }

        var retrieved = await _context.GasStationOptions.SingleAsync();
        retrieved.MonthlyRent = options.MonthlyRent;
        await _context.SaveChangesAsync();
    }
}
