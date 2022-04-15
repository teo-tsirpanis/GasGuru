using System.Text.Json.Serialization;

namespace GasGuru.Api;

public class MonthlyLedger
{
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    [JsonIgnore]
    public decimal Total => Income - Expenses;
}
