using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", nameof(GasGuru));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CardNumber).IsUnique();
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CardNumber).HasMaxLength(20).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}
