public static class PresentationServiceCollectionExtensions
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddExceptionHandler<GlobalExceptionHandler>();
    services.AddProblemDetails();

    return services;
  }
}
