using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;
using uvlo_api.Data.Models.Database;
using uvlo_api.Data.Models.View;
using uvlo_api.IRepos;

public class PostsRepo : IPostsRepo
{
  private readonly AppDbContext _dbContext;

  public PostsRepo(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken)
  {
    Post? post = await _dbContext.Posts
    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    return post;
  }

  public async Task<PostView?> GetPostViewAsync(int id, CancellationToken cancellationToken)
  {
    PostView? post = await _dbContext.Posts
      .Select(s => new PostView()
      {
        Id = s.Id,
        Title = s.Title,
        Content = s.Content,
        User = new UserView
        {
          Id = s.User.Id,
          UserName = s.User.UserName
        },
        CommentsCount = s.Comments.Count,
        CreatedAt = s.CreatedAt,
        UpdatedAt = s.UpdatedAt
      })
      .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    return post;
  }

  public async Task<List<PostView>> GetPostsViewAsync(CancellationToken cancellationToken)
  {
    IEnumerable<PostView> posts = await _dbContext.Posts
      .OrderByDescending(p => p.CreatedAt)
      .Select(p => new PostView
      {
        Id = p.Id,
        Title = p.Title,
        Content = p.Content,
        Comments = p.Comments.Take(5).Select(c => new PostCommentView
        {
          Id = c.Id,
          Content = c.Content,
          User = new UserView
          {
            Id = c.User.Id,
            UserName = c.User.UserName
          },
          CreatedAt = c.CreatedAt,
          UpdatedAt = c.UpdatedAt
        }).ToList(),
        CommentsCount = p.Comments.Count(),
        User = new UserView
        {
          Id = p.User.Id,
          UserName = p.User.UserName
        },
        UpdatedAt = p.UpdatedAt,
        CreatedAt = p.CreatedAt
      })
      .AsSplitQuery()
    .ToListAsync(cancellationToken);

    return posts.ToList();
  }

  public async Task<Post> Save(Post post, CancellationToken cancellationToken)
  {
    if (_dbContext.Entry(post).State == EntityState.Detached)
    {
      _dbContext.Posts.Add(post);
    }
    else
    {
      _dbContext.Posts.Update(post);
    }
    await _dbContext.SaveChangesAsync(cancellationToken);
    return post;
  }
}
