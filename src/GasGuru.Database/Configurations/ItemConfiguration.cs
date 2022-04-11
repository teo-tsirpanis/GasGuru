using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", nameof(GasGuru));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
        builder.Property(x => x.ItemType).IsRequired();
        builder.Property(x => x.Price).HasMonetaryPrecision().IsRequired();
        builder.Property(x => x.Cost).HasMonetaryPrecision().IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}
