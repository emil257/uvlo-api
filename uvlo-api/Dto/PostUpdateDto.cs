using System.ComponentModel.DataAnnotations;

namespace uvlo_api.Dto
{
  public class PostUpdateDto
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
  }
}