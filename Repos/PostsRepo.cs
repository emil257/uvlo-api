using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;
using uvlo_api.Data.Models.Database;

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
    int affectedRows = await _dbContext.Posts
                                .Where(p => p.Id == id && p.UserId == UserId)
                                .ExecuteUpdateAsync(p => p.SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                                );
    return affectedRows;
  }

  public async Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken)
  {
    Post? post = await _dbContext.Posts
     .Join(_dbContext.Users, p => p.UserId, u => u.Id, (post, user) => new { post, user })
     .Select(p => new Post
     {
       Id = p.post.Id,
       Title = p.post.Title,
       Content = p.post.Content,
       UserId = p.post.UserId,
       UpdatedAt = p.post.UpdatedAt,
       DeletedAt = p.post.DeletedAt,
       CreatedAt = p.post.CreatedAt,
       Comments = p.post.Comments.Take(5).ToList(),
       CommentsCount = p.post.Comments.Count,
       UserName = p.user.UserName,
     })
    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    return post;
  }

  public async Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken)
  {
    IEnumerable<Post> posts = await _dbContext.Posts
                                        .Join(_dbContext.Users, p => p.UserId, u => u.Id, (post, user) => new { post, user })
                                        .Select(p => new Post
                                        {
                                          Id = p.post.Id,
                                          Title = p.post.Title,
                                          Content = p.post.Content,
                                          UserId = p.post.UserId,
                                          UpdatedAt = p.post.UpdatedAt,
                                          DeletedAt = p.post.DeletedAt,
                                          CreatedAt = p.post.CreatedAt,
                                          Comments = p.post.Comments.Take(5).ToList(),
                                          CommentsCount = p.post.Comments.Count,
                                          UserName = p.user.UserName,
                                        })
                                        .AsSplitQuery()
                                        .ToListAsync(cancellationToken);

    return posts.ToList();
  }

  public async Task<int> UpdatePostAsync(PostUpdateDto post, int UserId, CancellationToken cancellationToken)
  {
    int affectedRows = await _dbContext.Posts
                                .Where(p => p.Id == post.Id && p.UserId == UserId)
                                .ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, post.Title)
                                .SetProperty(p => p.Content, post.Content)
                                .SetProperty(p => p.UpdatedAt, DateTimeOffset.UtcNow));
    return affectedRows;
  }

  public async Task<PostComment?> CreatePostCommentAsync(PostCommentCreateDto postComment, int UserId, CancellationToken cancellationToken)
  {
    Post? post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postComment.PostId, cancellationToken);

    if (post == null) { return null; }

    PostComment comment = new PostComment
    {
      UserId = UserId,
      PostId = postComment.PostId,
      Content = postComment.Content
    };

    _dbContext.PostComments.Add(comment);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return comment;
  }
}
