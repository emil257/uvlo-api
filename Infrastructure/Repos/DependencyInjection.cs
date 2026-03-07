public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddRepos(this IServiceCollection services)
  {
    services.AddScoped<IPostsRepo, PostsRepo>();

    return services;
  }
}
