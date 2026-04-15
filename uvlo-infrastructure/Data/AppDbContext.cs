using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uvlo_application.Abstractions.Data;
using uvlo_core.Entities;
using uvlo_core.Events;

public partial class AppDbContext : IdentityDbContext<AppUserModel, AppRoleModel, int>, IAppDbContext
{
  private readonly IPublisher _publisher;
  public DbSet<Post> Posts { get; set; } = null!;
  public DbSet<PostLike> PostLikes { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : base(options)
  {
    _publisher = publisher;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    int result = await base.SaveChangesAsync(cancellationToken);

    await PublishDomainEventsAsync();

    return result;
  }

  private async Task PublishDomainEventsAsync()
  {
    var domainEvents = ChangeTracker.Entries<Entity>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents != null && e.DomainEvents.Any())
        .SelectMany(e => {
          List<IDomainEvent> domainEvents = e.DomainEvents.ToList();

          e.ClearEvents();

          return domainEvents;
        })
        .ToList();

    foreach (IDomainEvent domainEvent in domainEvents)
    {
      await _publisher.Publish(domainEvent);
    }
  }
}
