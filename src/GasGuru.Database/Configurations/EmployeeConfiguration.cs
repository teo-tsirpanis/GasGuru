using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees", nameof(GasGuru));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
        builder.Property(x => x.HireDateStart).IsRequired();
        builder.Property(x => x.SalaryPerMonth).HasPrecision(8, 2).IsRequired();
        builder.Property(x => x.EmployeeType).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}
