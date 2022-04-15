#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GasGuru.Api;

[JsonSerializable(typeof(CustomerViewModel))]
[JsonSerializable(typeof(CustomerEditModel))]
[JsonSerializable(typeof(GasStationOptionsModel))]
[JsonSerializable(typeof(EmployeeViewModel))]
[JsonSerializable(typeof(EmployeeEditModel))]
[JsonSerializable(typeof(ItemViewModel))]
[JsonSerializable(typeof(ItemEditModel))]
[JsonSerializable(typeof(TransactionViewModel))]
[JsonSerializable(typeof(TransactionCreateModel))]
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public partial class ModelSerializerContext : JsonSerializerContext { }

public class GasStationOptionsModel
{
    [Range(0, 100_000_000.0)]
    public decimal MonthlyRent { get; set; }
}
