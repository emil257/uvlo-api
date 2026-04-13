using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;

namespace uvlo_application.Posts.GetById
{
  public sealed record GetByIdQuery(int id) : IQuery<PostView>;
}
