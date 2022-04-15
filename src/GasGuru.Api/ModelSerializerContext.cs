using System.Text.Json.Serialization;

namespace GasGuru.Api;

[JsonSerializable(typeof(CustomerViewModel))]
[JsonSerializable(typeof(CustomerEditModel))]
[JsonSerializable(typeof(EmployeeViewModel))]
[JsonSerializable(typeof(EmployeeEditModel))]
[JsonSerializable(typeof(ItemViewModel))]
[JsonSerializable(typeof(ItemEditModel))]
[JsonSerializable(typeof(TransactionViewModel))]
[JsonSerializable(typeof(TransactionCreateModel))]
[JsonSerializable(typeof(GasStationOptionsModel))]
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public partial class ModelSerializerContext : JsonSerializerContext { }
