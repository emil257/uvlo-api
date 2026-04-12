using Microsoft.Extensions.DependencyInjection;
using uvlo_application.Services;

public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<PostService>();
    services.AddScoped<CurrentUserService>();

    return services;
  }
}
