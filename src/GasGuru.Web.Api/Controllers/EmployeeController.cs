using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepo _repo;

    public EmployeeController(IEmployeeRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IAsyncEnumerable<EmployeeViewModel> Get([FromQuery] bool includeDeleted = false)
    {
        return _repo.GetAllAsync(includeDeleted);
    }

    [HttpGet("{id}")]
    public Task<EmployeeViewModel?> Get(Guid id)
    {
        return _repo.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] EmployeeEditModel model)
    {
        await _repo.CreateAsync(model);
    }

    [HttpPut("{id}")]
    public async Task Put(Guid id, [FromBody] EmployeeEditModel value)
    {
        await _repo.UpdateAsync(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
