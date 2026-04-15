using Microsoft.EntityFrameworkCore;
using uvlo_core.Entities;

namespace uvlo_application.Abstractions.Data
{
  public interface IAppDbContext
  {
    DbSet<Post> Posts { get; }
    DbSet<PostLike> PostLikes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}
