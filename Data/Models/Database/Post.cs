using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uvlo_api.Infrastructure.Exceptions;

public enum PostType
{
  Post = 0,
  Comment = 1
}

namespace uvlo_api.Data.Models.Database
{
  [Table("AppPosts")]
  public class Post : AppBaseEntity
  {
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
    public PostType Type { get; set; } = PostType.Post;
    public int? ParentId { get; set; } = null;
    public List<Post> Comments { get; set; } = new List<Post>();
    public Post(string content, int userId, PostType type, int? parentId = null)
    {
      if (string.IsNullOrWhiteSpace(content))
        throw new AppException("Content cannot be empty", 400);

      Type = type;
      Content = content;
      UserId = userId;
      ParentId = parentId;
    }
  }
}
