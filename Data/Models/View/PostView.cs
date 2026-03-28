using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data.Models.View
{
  public class PostView
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public UserView? User { get; set; } = null;
    public List<PostCommentView> Comments { get; set; } = new List<PostCommentView>();
    public int CommentsCount { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public PostView() { }
    public PostView(Post post)
    {
      Id = post.Id;
      Title = post.Title;
      Content = post.Content;
      if(post.User != null) User = new UserView(post.User);
      Comments = post.Comments.Select(c => new PostCommentView(c)).ToList();
      CreatedAt = post.CreatedAt;
      UpdatedAt = post.UpdatedAt;
    }
  }
}
