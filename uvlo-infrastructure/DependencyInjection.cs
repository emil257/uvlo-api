using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uvlo_application.IRepos;
using uvlo_infrastructure.Repos;


public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = configuration.GetConnectionString("REDIS_CONNECTIONSTRING");
    });
    services.AddDataProtection()
       .PersistKeysToFileSystem(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.aspnet/DataProtection-Keys"));

    services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("PSQL_CONNECTIONSTRING"))
    .EnableSensitiveDataLogging());

    services.AddSingleton<ITicketStore, AppTicketStore>();

    services.AddScoped<IPostsRepo, PostsRepo>();

    return services;
  }
}
