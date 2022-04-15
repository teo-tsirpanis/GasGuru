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
    public Task<CustomerViewModel?> Get(Guid id)
    {
        return _repo.GetByIdAsync(id);
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
