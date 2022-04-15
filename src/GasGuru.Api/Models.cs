#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GasGuru.Api;

[JsonSerializable(typeof(CustomerModel))]
[JsonSerializable(typeof(GasStationOptionsModel))]
[JsonSerializable(typeof(EmployeeModel))]
[JsonSerializable(typeof(ItemModel))]
[JsonSerializable(typeof(TransactionModel))]
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public partial class ModelSerializerContext : JsonSerializerContext { }

public class CustomerModel
{
    public Guid Id { get; init; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required, RegularExpression("^A.+$")]
    public string CardNumber { get; set; }
    public bool IsDeleted { get; set; }
}

public class GasStationOptionsModel
{
    [Range(0, 100_000_000.0)]
    public decimal MonthlyRent { get; set; }
}

// These enums are already defined in the Entities project, but are defined here
// again, to further separate the database's domain model from the web API's.
// New member additions must be replicated there accordingly.
public enum EmployeeType
{
    Manager,
    Staff,
    Cashier
}

public class EmployeeModel
{
    public Guid Id { get; init; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public DateTime HireDateStart { get; set; }
    public DateTime? HireDateEnd { get; set; }
    [Range(0.01, 100_000_000.0)]
    public decimal SalaryPerMonth { get; set; }
    public EmployeeType EmployeeType { get; set; }
}

public enum ItemType
{
    Fuel,
    Product,
    Service
}

public class ItemModel
{
    public Guid Id { get; init; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    [Range(0.01, 100_000_000.0)]
    public decimal Price { get; set; }
    [Range(0.01, 100_000_000.0)]
    public decimal Cost { get; set; }
    public bool IsDeleted { get; set; }
}

public enum PaymentMethod
{
    CreditCard,
    Cash
}

public class TransactionModel
{
    public Guid Id { get; init; }
    public DateTime Date { get; set; }
    public string EmployeeDisplay { get; set; }
    public Guid EmployeeId { get; set; }
    public string CustomerDisplay { get; set; }
    public Guid CustomerId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
#nullable enable
    public List<TransactionLineModel>? Lines { get; set; }
#nullable disable
}

public class TransactionLineModel
{
    public Guid Id { get; init; }
    public Guid ItemId { get; set; }
    public string ItemDisplay { get; set; }
    [Range(1, 1000)]
    public int Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    [JsonIgnore]
    public decimal NetPrice => Quantity * ItemPrice;
    public decimal DiscountPercent { get; set; }
    [JsonIgnore]
    public decimal DiscountValue => NetPrice * DiscountPercent;
    [JsonIgnore]
    public decimal TotalPrice => NetPrice - DiscountValue;
}
