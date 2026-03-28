using uvlo_api.Data.Models.Database;
using uvlo_api.Data.Models.View;
using uvlo_api.Infrastructure.Exceptions;

namespace uvlo_api.Services
{
  public class PostService
  {
    private readonly IPostsRepo _postsRepo;
    public PostService(IPostsRepo postsRepo)
    {
      _postsRepo = postsRepo;
    }

    public async Task<PostView> CreatePostAsync(string title, string content, int UserId, CancellationToken cancellationToken)
    {
      Post post = new Post(title, content, UserId);

      return new PostView(await _postsRepo.Save(post, cancellationToken));
    }

    public async Task<PostView> GetPostById(int id, CancellationToken cancellationToken)
    {
      PostView? post = await _postsRepo.GetPostViewAsync(id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      return post;
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
      post.Title = title;
      post.Content = content;
      await _postsRepo.Save(post, cancellationToken);
      return new PostView(post);
    }

    public async Task<List<PostView>> GetPostsAsync(CancellationToken cancellationToken)
    {
      return await _postsRepo.GetPostsViewAsync(cancellationToken);
    }
    public async Task<PostCommentView> CreatePostComment(string content, int userId, int postId, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(postId, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      var comment = post.AddComment(content, userId);
      await _postsRepo.Save(post, cancellationToken);
      return new PostCommentView(comment);
    }
  }
}
