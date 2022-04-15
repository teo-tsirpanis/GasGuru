using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/monthly-ledger")]
[ApiController]
public class MonthlyLedgerController : ControllerBase
{
    private readonly IMonthlyLedgerRepo _repo;

    public MonthlyLedgerController(IMonthlyLedgerRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<MonthlyLedger>> Get([FromQuery] int month, [FromQuery] int year)
    {
        return await _repo.GetMonthlyLedgerAsync(month, year);
    }
}
