using uvlo_api.Services;

public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<PostService>();

    return services;
  }
}
