#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

public class CustomerViewModel
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CardNumber { get; set; }
    public bool IsDeleted { get; set; }
}

public class CustomerEditModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required, RegularExpression("^A.+$")]
    public string CardNumber { get; set; }
}
