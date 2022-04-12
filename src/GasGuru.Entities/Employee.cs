namespace GasGuru.Entities;

public class Employee
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime HireDateStart { get; set; }
    public DateTime? HireDateEnd { get; set; }
    public decimal SalaryPerMonth { get; set; }
    public EmployeeType EmployeeType { get; set; }

    public Employee(string name, string surname, decimal salaryPerMonth, EmployeeType employeeType)
    {
        Name = name;
        Surname = surname;
        HireDateStart = DateTime.Now;
        SalaryPerMonth = salaryPerMonth;
        EmployeeType = employeeType;
    }
}
