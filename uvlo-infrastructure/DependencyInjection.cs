using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uvlo_infrastructure.Repos;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Abstractions.Auth;
using uvlo_infrastructure.Auth;
using uvlo_application.Abstractions.Data;
using uvlo_application.Abstractions.Services;
using uvlo_infrastructure.Services;


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

    services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

    services.AddSingleton<ITicketStore, AppTicketStore>();

    services.AddScoped<IPostsRepo, PostsRepo>();
    services.AddScoped<IUserContext, UserContext>();
    services.AddScoped<IFileUploadService, FileUploadService>();

    return services;
  }
}
