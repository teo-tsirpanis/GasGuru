using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GasGuru.Database.Configurations;

internal static class ConfigurationExtensions
{
    public static PropertyBuilder<TProperty> HasMonetaryPrecision<TProperty>(this PropertyBuilder<TProperty> property) =>
        property.HasPrecision(8, 2);
}
