namespace GasGuru.Entities;

public class Customer
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CardNumber { get; set; }

    public Customer(string name, string surname, string cardNumber)
    {
        Name = name;
        Surname = surname;
        CardNumber = cardNumber;
    }
}
