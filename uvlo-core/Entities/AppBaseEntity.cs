namespace uvlo_core.Entities
{
  public abstract class AppBaseEntity : Entity
  {
    public int Id { get;  set; }
    public int UserId { get;  set; }
    public AppUserModel User { get;  set; } = null!;
    public DateTimeOffset CreatedAt { get;  set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get;  set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get;  set; } = null;

    public void SoftDelete() => DeletedAt = DateTimeOffset.UtcNow;
  }
}
