using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepo _repo;

    public CustomerController(ICustomerRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IAsyncEnumerable<CustomerViewModel> Get([FromQuery] bool includeDeleted = false)
    {
        return _repo.GetAllAsync(includeDeleted);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerViewModel>> Get(Guid id)
    {
        CustomerViewModel? customer = await _repo.GetByIdAsync(id);

        if (customer == null)
            return NotFound();
        return customer;
    }

    [HttpGet("cardNumber/{cardNumber}")]
    public async Task<ActionResult<CustomerViewModel>> Get(string cardNumber)
    {
        CustomerViewModel? customer = await _repo.GetByCardNumberAsync(cardNumber);

        if (customer == null)
            return NotFound();
        return customer;
    }

    [HttpPost]
    public async Task Post([FromBody] CustomerEditModel model)
    {
        await _repo.CreateAsync(model);
    }

    [HttpPut("{id}")]
    public async Task Put(Guid id, [FromBody] CustomerEditModel value)
    {
        await _repo.UpdateAsync(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
