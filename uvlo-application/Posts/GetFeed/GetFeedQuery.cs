using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;

namespace uvlo_application.Posts.GetFeed
{
  public sealed record GetFeedQuery() : IQuery<List<PostView>>;
}
