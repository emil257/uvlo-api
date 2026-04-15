
using uvlo_core.Entities;

namespace uvlo_core.Events
{
  public sealed record PostCreatedDomainEvent(Post post) : IDomainEvent;
}
