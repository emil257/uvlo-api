namespace uvlo_core.Entities
{
  public class PostLike
  {
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public AppUserModel User { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public PostLike(int postId, int userId)
    {
      PostId = postId;
      UserId = userId;
    }
  }
}
