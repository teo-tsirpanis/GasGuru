using GasGuru.Api;
using GasGuru.Database;
using GasGuru.Database.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddGasGuruDatabase(this IServiceCollection services)
    {
        return services
            .AddDbContext<GasStationContext>()
            .AddTransient<IEntityRepo<CustomerModel>, CustomerRepo>()
            ;
    }
}
