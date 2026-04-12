using uvlo_application.Exceptions;
using uvlo_application.IRepos;
using uvlo_application.View;
using uvlo_core.Entities;
using uvlo_core.Enums;

namespace uvlo_application.Services
{
  public class PostService
  {
    private readonly IPostsRepo _postsRepo;
    public PostService(IPostsRepo postsRepo)
    {
      _postsRepo = postsRepo;
    }

    public async Task<PostView> CreatePostAsync(string content, int? parentId, int UserId, CancellationToken cancellationToken)
    {
      if(parentId != null)
      {
        Post? parentPost = await _postsRepo.GetPostAsync(parentId.Value, cancellationToken);
        if (parentPost == null)
          throw new AppException("Parent post not found", 404);
      }

      Post post = new Post(content, UserId, parentId == null ? PostType.Post : PostType.Comment, parentId);

      return new PostView(await _postsRepo.Save(post, cancellationToken));
    }

    public async Task<PostView> GetPostById(int id, CancellationToken cancellationToken)
    {
      PostView? post = await _postsRepo.GetPostViewAsync(id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      return post;
    }
    public async Task<IEnumerable<PostView>> GetPostChildrenById(int parentId, CancellationToken cancellationToken)
    {
        return await _postsRepo.GetPostsViewAsync(parentId, cancellationToken);
    }
    public async Task<PostView> DeletePost(int id, int UserId, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      if(post.UserId != UserId)
        throw new AppException("Unauthorized", 403);
      post.SoftDelete();
      await _postsRepo.Save(post, cancellationToken);
      return new PostView(post);
    }
    public async Task<PostView> UpdatePost(int id, string title, string content, int UserId, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      if(post.UserId != UserId)
        throw new AppException("Unauthorized", 403);
      post.Content = content;
      await _postsRepo.Save(post, cancellationToken);
      return new PostView(post);
    }

    public async Task<List<PostView>> GetPostsAsync(CancellationToken cancellationToken)
    {
      return await _postsRepo.GetPostsViewAsync(null, cancellationToken);
    }
  }
}
