using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal class TransactionLineConfiguration : IEntityTypeConfiguration<TransactionLine>
{
    public void Configure(EntityTypeBuilder<TransactionLine> builder)
    {
        builder.ToTable("TransactionLines", nameof(GasGuru));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        // The Transaction field is configured on TransactionConfiguration.
        builder.HasOne(x => x.Item).WithMany().HasForeignKey(x => x.ItemId).IsRequired();
        builder.Property(x => x.Quantity).HasPrecision(6, 2).IsRequired();
        builder.Property(x => x.ItemPrice).HasMonetaryPrecision().IsRequired();
        builder.Property(x => x.DiscountPercent).HasMonetaryPrecision().IsRequired();
        builder.Ignore(x => x.NetPrice);
        builder.Ignore(x => x.DiscountValue);
        builder.Ignore(x => x.TotalPrice);
    }
}
