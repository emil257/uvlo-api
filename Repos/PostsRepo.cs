using Microsoft.EntityFrameworkCore;

using uvlo_api.Data.Models.Database;
using uvlo_api.Data;

public class PostsRepo : IPostsRepo
{
  private AppDbContext _dbContext;

  public PostsRepo(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Post> CreatePostAsync(PostCreateDto postCreateDto, int UserId, CancellationToken cancellationToken)
  {
    Post post = new Post
    {
      UserId = UserId,
      Title = postCreateDto.Title,
      Content = postCreateDto.Content
    };

    _dbContext.Posts.Add(post);
    await _dbContext.SaveChangesAsync(cancellationToken);

    return post;
  }

  public async Task<int> DeletePostAsync(int id, int UserId, CancellationToken cancellationToken)
  {
    int affectedRows = await _dbContext.Posts.Where(p => p.Id == id && p.UserId == UserId)
                                .ExecuteUpdateAsync(p => p.SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                                );
    return affectedRows;
  }

  public async Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken)
  {
    Post? post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null, cancellationToken);
    return post;
  }

  public async Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken)
  {
    IEnumerable<Post> posts = await _dbContext.Posts.Where(p => p.DeletedAt == null).ToListAsync(cancellationToken);

    return posts.ToList();
  }

  public async Task<int> UpdatePostAsync(PostUpdateDto post, int UserId, CancellationToken cancellationToken)
  {
    int affectedRows = await _dbContext.Posts.Where(p => p.Id == post.Id && p.UserId == UserId && p.DeletedAt == null)
																.ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, post.Title)
                                .SetProperty(p => p.Content, post.Content)
                                .SetProperty(p => p.UpdatedAt, DateTimeOffset.UtcNow));
    return affectedRows;
  }
}
