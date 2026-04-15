using System;
using System.Collections.Generic;
using System.Text;
using uvlo_core.Entities;

namespace uvlo_core.Events
{
  public sealed record PostLikedDomainEvent(Post post, int UserId) : IDomainEvent;
}
