using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;
using uvlo_core.Entities;

namespace uvlo_application.Posts.Like
{
  public record LikePostCommand(int PostId) : ICommand<PostView>;
}
