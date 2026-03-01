using Microsoft.EntityFrameworkCore;

public static partial class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddCachingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("REDIS_CONNECTIONSTRING");
        });

        return services;
    }
}
