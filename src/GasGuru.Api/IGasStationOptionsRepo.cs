namespace GasGuru.Api;

public interface IGasStationOptionsRepo
{
    Task<GasStationOptionsModel> GetOptionsAsync();
    Task UpdateOptionsAsync(GasStationOptionsModel options);
}
