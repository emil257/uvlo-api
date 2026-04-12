using Microsoft.EntityFrameworkCore;
using uvlo_application.IRepos;
using uvlo_application.View;
using uvlo_core.Entities;


namespace uvlo_infrastructure.Repos
{
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
        .Where(p => p.Id == id)
        .Select(p => new PostView(p.Id,p.Content, p.User, p.Comments.Count(), p.CreatedAt, p.UpdatedAt))
        .FirstOrDefaultAsync(cancellationToken);
      return post;
    }

    public async Task<List<PostView>> GetPostsViewAsync(int? parentId, CancellationToken cancellationToken)
    {
      IQueryable<PostView> posts = _dbContext.Posts
        .OrderByDescending(p => p.CreatedAt)
        .Where(p => p.ParentId == parentId)
        .Select(p => new PostView(p.Id, p.Content, p.User, p.Comments.Count(), p.CreatedAt, p.UpdatedAt))
        .AsSplitQuery();

      return await posts.ToListAsync(cancellationToken);
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

}
