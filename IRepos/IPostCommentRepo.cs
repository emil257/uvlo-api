using uvlo_api.Data.Models.Database;
using uvlo_api.Data.Models.View;

namespace uvlo_api.IRepos
{
  public interface IPostCommentRepo
  {
    Task<List<PostCommentView>> GetPostCommentsViewAsync(int postId, CancellationToken cancellationToken);
    Task<PostCommentView?> GetPostCommentViewAsync(int id, CancellationToken cancellationToken);
    Task<PostComment?> GetPostCommentAsync(int id, CancellationToken cancellationToken);
    Task<PostComment> Save(PostComment postComment, CancellationToken cancellationToken);
  }
}
