using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;
using uvlo_api.Data.Models.Database;
using uvlo_api.Data.Models.View;
using uvlo_api.IRepos;

namespace uvlo_api.Repos
{
  public class PostCommentRepo : IPostCommentRepo
  {
    private readonly AppDbContext _dbContext;

    public PostCommentRepo(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task<PostComment?> GetPostCommentAsync(int id, CancellationToken cancellationToken)
    {
      PostComment? postComment = await _dbContext.PostComments.FirstOrDefaultAsync(pc => pc.Id == id);
      return postComment;
    }

    public async Task<List<PostCommentView>> GetPostCommentsViewAsync(int postId, CancellationToken cancellationToken)
    {
      List<PostCommentView> postCommentView = await _dbContext.PostComments
        .Where(pc => pc.PostId == postId)
        .OrderByDescending(p => p.CreatedAt)
        .Select(pc => new PostCommentView
        {
          Id = pc.Id,
          Content = pc.Content,
          User = new UserView
          {
            Id = pc.User.Id,
            UserName = pc.User.UserName
          },
          CreatedAt = pc.CreatedAt,
          UpdatedAt = pc.UpdatedAt,
        }).ToListAsync();
      return postCommentView;
    }

    public async Task<PostCommentView?> GetPostCommentViewAsync(int id, CancellationToken cancellationToken)
    {
      PostCommentView? postCommentView = await _dbContext.PostComments.Select(pc => new PostCommentView
      {
        Id = pc.Id,
        Content = pc.Content,
        User = new UserView
        {
          Id = pc.User.Id,
          UserName = pc.User.UserName
        },
        CreatedAt = pc.CreatedAt,
        UpdatedAt = pc.UpdatedAt,
      }).FirstOrDefaultAsync(pc => pc.Id == 1);
      return postCommentView;
    }

    public async Task<PostComment> Save(PostComment postComment, CancellationToken cancellationToken)
    {
      if (_dbContext.Entry(postComment).State == EntityState.Detached)
      {
        _dbContext.PostComments.Add(postComment);
      }
      else
      {
        _dbContext.PostComments.Update(postComment);
      }
      await _dbContext.SaveChangesAsync(cancellationToken);
      return postComment;
    }
  }
}
