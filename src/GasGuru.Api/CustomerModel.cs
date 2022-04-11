#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GasGuru.Api;

public class CustomerModel
{
    public Guid Id { get; init; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required, RegularExpression("^A.+$")]
    public string CardNumber { get; set; }
    public bool IsDeleted { get; set; }
}
