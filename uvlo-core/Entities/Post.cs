using uvlo_core.Exeptions;
using uvlo_core.Enums;

namespace uvlo_core.Entities
{
  public class Post : AppBaseEntity
  {
    public string Content { get; set; } = string.Empty;
    public PostType Type { get; set; } = PostType.Post;
    public int? ParentId { get; set; } = null;
    public List<Post> Comments { get; set; } = new List<Post>();
    public int LikesCount { get; set; } = 0;
    public Post(string content, int userId, PostType type, int? parentId = null)
    {
      if (string.IsNullOrWhiteSpace(content))
        throw new PostException("Content cannot be empty", 400);
      Type = type;
      Content = content;
      UserId = userId;
      ParentId = parentId;
    }
  }
}
