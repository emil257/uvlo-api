using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uvlo_api.Data.Models.Database
{
  [Table("AppPosts")]
  public class Post
  {
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public List<PostComment> Comments { get; set; } = new List<PostComment>();
    [NotMapped]
    public int CommentsCount { get; set; } = 0;
    [NotMapped]
    public string? UserName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
  }
}
[Table("AppPostComments")]
public class PostComment
{
  [Key]
  public int Id { get; set; }
  [MaxLength(500)]
  public string Content { get; set; } = string.Empty;
  public int UserId { get; set; }
  public int PostId { get; set; }
  public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
  public DateTimeOffset? DeletedAt { get; set; } = null;
}
public class PostCommentCreateDto
{
  [Required]
  public int PostId { get; set; }
  [Required]
  public string Content { get; set; } = string.Empty;
}

public class PostCreateDto
{
  [Required]
  public string Title { get; set; } = string.Empty;
  [Required]
  public string Content { get; set; } = string.Empty;
}

public class PostUpdateDto
{
  [Required]
  public int Id { get; set; }
  [Required]
  public string Title { get; set; } = string.Empty;
  [Required]
  public string Content { get; set; } = string.Empty;
}
