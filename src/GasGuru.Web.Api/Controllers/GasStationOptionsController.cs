using GasGuru.Api;
using Microsoft.AspNetCore.Mvc;

namespace GasGuru.Web.Api.Controllers;

[Route("api/options")]
[ApiController]
public class GasStationOptionsController : ControllerBase
{
    private readonly IGasStationOptionsRepo _repo;

    public GasStationOptionsController(IGasStationOptionsRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public Task<GasStationOptionsModel> Get()
    {
        return _repo.GetOptionsAsync();
    }

    [HttpPut]
    public async Task Put([FromBody] GasStationOptionsModel model)
    {
        await _repo.UpdateOptionsAsync(model);
    }
}
