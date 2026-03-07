using uvlo_api.Data.Models.Database;

public interface IPostsRepo
{
  Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken);
  Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken);
  Task<Post> CreatePostAsync(PostCreateDto post, int UserId, CancellationToken cancellationToken);
  Task<int> DeletePostAsync(int id, int UserId, CancellationToken cancellationToken);
  Task<int> UpdatePostAsync(PostUpdateDto post, int UserId, CancellationToken cancellationToken);
}
