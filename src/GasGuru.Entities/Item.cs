namespace GasGuru.Entities;

public class Item
{
    public Guid Id { get; }
    public string Code { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public bool IsDeleted { get; set; }

    public Item(string code, string description, ItemType itemType, decimal price, decimal cost)
    {
        Code = code;
        Description = description;
        ItemType = itemType;
        Price = price;
        Cost = cost;
    }
}
