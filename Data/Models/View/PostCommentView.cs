using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data.Models.View
{
  public class PostCommentView
  {
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public UserView? User { get; set; } = null;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public PostCommentView() { }
    public PostCommentView(PostComment comment)
    {
      Id = comment.Id;
      Content = comment.Content;
      if(comment.User != null) User = new UserView(comment.User);
      CreatedAt = comment.CreatedAt;
      UpdatedAt = comment.UpdatedAt;
    }
  }
}
