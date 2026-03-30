using uvlo_api.IRepos;
using uvlo_api.Repos;

public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddRepos(this IServiceCollection services)
  {
    services.AddScoped<IPostsRepo, PostsRepo>();
    services.AddScoped<IPostCommentRepo, PostCommentRepo>();

    return services;
  }
}
