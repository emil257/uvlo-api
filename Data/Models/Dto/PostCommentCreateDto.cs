using System.ComponentModel.DataAnnotations;

namespace uvlo_api.Data.Models.Dto
{
  public class PostCommentCreateDto
  {
    [Required]
    public int PostId { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;
  }
}
