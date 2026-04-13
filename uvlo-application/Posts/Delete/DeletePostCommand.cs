using System;
using System.Collections.Generic;
using System.Text;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.View;

namespace uvlo_application.Posts.Delete
{
  public record DeletePostCommand(int id) : ICommand<PostView>;

}
