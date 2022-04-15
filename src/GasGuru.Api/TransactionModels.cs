#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GasGuru.Api;

public enum PaymentMethod
{
    CreditCard,
    Cash
}

public class TransactionViewModel
{
    public Guid Id { get; init; }
    public DateTime Date { get; set; }
    public string EmployeeDisplay { get; set; }
    public Guid EmployeeId { get; set; }
    public string CustomerDisplay { get; set; }
    public Guid CustomerId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
#nullable enable
    public List<TransactionLineViewModel>? Lines { get; set; }
#nullable disable
    [JsonIgnore]
    public decimal TotalValue => Lines?.Sum(l => l.TotalPrice) ?? 0;
}

public class TransactionLineViewModel
{
    public Guid Id { get; init; }
    public Guid ItemId { get; set; }
    public string ItemDisplay { get; set; }
    public decimal Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    [JsonIgnore]
    public decimal NetPrice => Quantity * ItemPrice;
    public decimal DiscountPercent { get; set; }
    [JsonIgnore]
    public decimal DiscountValue => NetPrice * DiscountPercent;
    [JsonIgnore]
    public decimal TotalPrice => NetPrice - DiscountValue;
}

public class TransactionCreateModel
{
    public Guid EmployeeId { get; set; }
    public Guid CustomerId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
#nullable enable
    public List<TransactionLineCreateModel>? Lines { get; set; }
#nullable disable
}

public class TransactionLineCreateModel
{
    public Guid Id { get; init; }
    public Guid ItemId { get; set; }
    [Range(0.01, 1000)]
    public decimal Quantity { get; set; }
}
