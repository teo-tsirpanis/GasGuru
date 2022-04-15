using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/transaction")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepo _repo;

    public TransactionController(ITransactionRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IAsyncEnumerable<TransactionViewModel> Get()
    {
        return _repo.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionViewModel>> Get(Guid id)
    {
        TransactionViewModel? transaction = await _repo.GetByIdAsync(id);
        if (transaction == null)
            return NotFound();
        return transaction;
    }

    [HttpPost]
    public async Task Post([FromBody] TransactionCreateModel model)
    {
        await _repo.CreateAsync(model);
    }
}
