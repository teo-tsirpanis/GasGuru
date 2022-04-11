using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal class GasStationOptionsConfiguration : IEntityTypeConfiguration<GasStationOptions>
{
    public void Configure(EntityTypeBuilder<GasStationOptions> builder)
    {
        builder.ToTable(nameof(GasStationOptions), nameof(GasGuru));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.MonthlyRent).HasMonetaryPrecision().IsRequired();
        builder.HasData(new GasStationOptions() { MonthlyRent = 5000 });
    }
}
