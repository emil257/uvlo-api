using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using uvlo_core.Events;

namespace uvlo_application.EventHandlers
{
  internal sealed class PostCreatedEventHandler : INotificationHandler<PostCreatedDomainEvent>
  {
    private readonly ILogger _logger;

    public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(PostCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("---------------------------------");
      _logger.LogInformation("Post created with ID: {PostId} ({PostType})", notification.post.Id, notification.post.Type);
      _logger.LogInformation("---------------------------------");
      
      return Task.CompletedTask;
    }
  }
}
