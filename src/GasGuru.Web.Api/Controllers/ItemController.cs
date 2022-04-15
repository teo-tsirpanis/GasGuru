using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/item")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemRepo _repo;

    public ItemController(IItemRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IAsyncEnumerable<ItemViewModel> Get([FromQuery] bool includeDeleted = false)
    {
        return _repo.GetAllAsync(includeDeleted);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemViewModel>> Get(Guid id)
    {
        ItemViewModel? item = await _repo.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return item;
    }

    [HttpPost]
    public async Task Post([FromBody] ItemEditModel model)
    {
        await _repo.CreateAsync(model);
    }

    [HttpPut("{id}")]
    public async Task Put(Guid id, [FromBody] ItemEditModel value)
    {
        await _repo.UpdateAsync(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
