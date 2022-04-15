namespace GasGuru.Entities;

public class TransactionLine
{
    private Transaction? _transaction;
    private Item? _item;

    public Guid Id { get; }
    public Transaction Transaction
    {
        get => _transaction ?? throw Utilities.CreateUnboundValueAccessException();
        set => _transaction = value;
    }
    public Guid TransactionId { get; set; }
    public Item Item
    {
        get => _item ?? throw Utilities.CreateUnboundValueAccessException();
        set
        {
            _item = value;
            ItemPrice = value.Price;
        }
    }
    public Guid ItemId { get; set; }
    // The quantity can be a decimal only for fuels.
    public decimal Quantity { get; set; }
    // We can't just forward it to Price.Item because it might change in the meantime.
    public decimal ItemPrice { get; set; }
    public decimal NetPrice => Quantity * ItemPrice;
    public decimal DiscountPercent { get; set; }
    public decimal DiscountValue => NetPrice * DiscountPercent;
    public decimal TotalPrice => NetPrice - DiscountValue;
}
