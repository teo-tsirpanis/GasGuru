using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

public class GasStationOptionsModel
{
    [Range(0, 100_000_000.0)]
    public decimal MonthlyRent { get; set; }
}
