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
    public Guid EmployeeId { get; set; }
    public Customer Customer
    {
        get => _customer ?? throw Utilities.CreateUnboundValueAccessException();
        set => _customer = value;
    }
    public Guid CustomerId { get; set; }
    public PaymentMethod PaymentMethod { get; }
    public List<TransactionLine> Lines { get; } = new();
    public decimal TotalValue => Lines.Sum(l => l.TotalPrice);

    public Transaction(DateTime date, PaymentMethod paymentMethod)
    {
        Date = date;
        PaymentMethod = paymentMethod;
    }
}
