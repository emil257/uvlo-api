using uvlo_core.Entities;
using uvlo_application.View;

namespace uvlo_application.Abstractions.Repos
{
  public interface IPostsRepo
  {
    Task<List<PostView>> GetPostsViewAsync(int? parentId, CancellationToken cancellationToken);
    Task<PostView?> GetPostViewAsync(int id, CancellationToken cancellationToken);
    Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken);
    Task<Post> Save(Post post, CancellationToken cancellationToken);
  }
}
