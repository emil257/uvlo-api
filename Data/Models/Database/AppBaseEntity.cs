using System.ComponentModel.DataAnnotations;

namespace uvlo_api.Data.Models.Database
{
  public abstract class AppBaseEntity
  {
    [Key]
    public int Id { get; protected set; }
    public int UserId { get; protected set; }
    public AppUserModel User { get; protected set; } = null!;
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; protected set; } = null;

    public void SoftDelete() => DeletedAt = DateTimeOffset.UtcNow;
  }
}
