using Microsoft.AspNetCore.Http;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;

namespace uvlo_application.Posts.Create
{
  public record CreatePostCommand(string content,int? parentId, List<IFormFile>? attachments) : ICommand<PostView>;
}
