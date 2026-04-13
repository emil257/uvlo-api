using Microsoft.Extensions.DependencyInjection;

public static partial class ApplicationServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {

    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);
    });

    return services;
  }
}
