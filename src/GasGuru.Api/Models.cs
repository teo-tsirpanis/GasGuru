#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

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
