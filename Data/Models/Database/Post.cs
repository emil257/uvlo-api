using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uvlo_api.Infrastructure.Exceptions;

namespace uvlo_api.Data.Models.Database
{
  [Table("AppPosts")]
  public class Post : AppBaseEntity
  {
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
    public List<PostComment> Comments { get; set; } = new List<PostComment>();
    public Post(string title, string content, int userId)
    {
      if (string.IsNullOrWhiteSpace(title))
        throw new AppException("Title cannot be empty", 400);
      if (string.IsNullOrWhiteSpace(content))
        throw new AppException("Content cannot be empty", 400);

      Title = title;
      Content = content;
      UserId = userId;
    }
    public PostComment AddComment(string content, int userId)
    {
      var comment = new PostComment(content, userId, Id);
      Comments.Add(comment);
      return comment;
    }
  }
}
