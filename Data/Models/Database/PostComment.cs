using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uvlo_api.Infrastructure.Exceptions;

namespace uvlo_api.Data.Models.Database
{
  [Table("AppPostComments")]
  public class PostComment : AppBaseEntity
  {
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
    public int PostId { get; set; }
    public PostComment(string content, int userId, int postId)
    {
      if (string.IsNullOrWhiteSpace(content))
        throw new AppException("Content cannot be empty", 400);

      Content = content;
      UserId = userId;
      PostId = postId;
    }
  }
  public class PostCommentCreateDto
  {
    [Required]
    public int PostId { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;
  }
}
