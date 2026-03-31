using Microsoft.AspNetCore.Mvc;
using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data.Models.View
{
  public class PostView
  {
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public UserView? User { get; set; } = null;
    public List<PostView> Comments { get; set; } = new List<PostView>();
    public int CommentsCount { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public PostView() { }
    public PostView(Post post)
    {
      Id = post.Id;
      Content = post.Content;
      if(post.User != null) User = new UserView(post.User.Id, post.User.UserName);
      Comments = post.Comments.Select(c => new PostView(c)).ToList();
      CreatedAt = post.CreatedAt;
      UpdatedAt = post.UpdatedAt;
    }
    public PostView(int id, string content, AppUserModel user, int commentsCount, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
      Id = id;
      Content = content;
      User = new UserView(user.Id, user.UserName);
      CommentsCount = commentsCount;
      CreatedAt = createdAt;
      UpdatedAt = updatedAt;
    }
  }
}
