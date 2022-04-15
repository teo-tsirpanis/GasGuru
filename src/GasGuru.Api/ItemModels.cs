#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

public enum ItemType
{
    Fuel,
    Product,
    Service
}

public class ItemViewModel
{
    public Guid Id { get; init; }
    public string Code { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public bool IsDeleted { get; set; }
}

public class ItemEditModel
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    [Range(0.01, 100_000_000.0)]
    public decimal Price { get; set; }
    [Range(0.01, 100_000_000.0)]
    public decimal Cost { get; set; }
}
