using uvlo_api.Data.Models.Database;
using uvlo_api.Data.Models.View;

namespace uvlo_api.IRepos
{
  public interface IPostsRepo
  {
    Task<List<PostView>> GetPostsViewAsync(int? parentId, CancellationToken cancellationToken);
    Task<PostView?> GetPostViewAsync(int id, CancellationToken cancellationToken);
    Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken);
    Task<Post> Save(Post post, CancellationToken cancellationToken);
  }
}
