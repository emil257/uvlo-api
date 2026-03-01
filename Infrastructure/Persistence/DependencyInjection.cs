using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;

public static partial class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("PSQL_CONNECTIONSTRING"))
        .EnableSensitiveDataLogging());

        return services;
    }
}
