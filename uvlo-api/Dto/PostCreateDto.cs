using System.ComponentModel.DataAnnotations;

namespace uvlo_api.Dto
{
  public class PostCreateDto
  {
    [Required]
    public string Content { get; set; } = string.Empty;
    public int? ParentId { get; set; } = null;
  }
}