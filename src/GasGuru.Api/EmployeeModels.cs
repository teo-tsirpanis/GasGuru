#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

// These enums are already defined in the Entities project, but are defined here
// again, to further separate the database's domain model from the web API's.
// New member additions must be replicated there accordingly.
public enum EmployeeType
{
    Manager,
    Staff,
    Cashier
}

public class EmployeeViewModel
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

public class EmployeeEditModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal SalaryPerMonth { get; set; }
    public EmployeeType EmployeeType { get; set; }
}
