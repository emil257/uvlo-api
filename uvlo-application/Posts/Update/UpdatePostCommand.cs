using System;
using System.Collections.Generic;
using System.Text;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;

namespace uvlo_application.Posts.Update
{
  public record UpdatePostCommand(int id, string content) : ICommand<PostView>;
}
