namespace GasGuru.Entities;

public class Transaction
{
    private Employee? _employee;
    private Customer? _customer;

    public Guid Id { get; }
    public DateTime Date { get; }
    public Employee Employee
    {
        get => _employee ?? throw Utilities.CreateUnboundValueAccessException();
        set => _employee = value;
    }
    public Customer Customer
    {
        get => _customer ?? throw Utilities.CreateUnboundValueAccessException();
        set => _customer = value;
    }
    public PaymentMethod PaymentMethod { get; }
    public List<TransactionLine> Lines { get; } = new();

    public Transaction(DateTime date, PaymentMethod paymentMethod)
    {
        Date = date;
        PaymentMethod = paymentMethod;
    }
}
