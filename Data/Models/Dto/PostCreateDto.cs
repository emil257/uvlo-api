using System.ComponentModel.DataAnnotations;

namespace uvlo_api.Data.Models.Dto
{
  public class PostCreateDto
  {
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
  }
}