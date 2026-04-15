using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using uvlo_core.Events;

namespace uvlo_application.EventHandlers
{
  internal sealed class PostLikedEventHandler : INotificationHandler<PostLikedDomainEvent>
  {
    private readonly ILogger _logger;

    public PostLikedEventHandler(ILogger<PostCreatedEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(PostLikedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("---------------------------------");
      _logger.LogInformation("Post liked with ID: {PostId} ({PostType}) by user {UserId}", notification.post.Id, notification.post.Type, notification.UserId);
      _logger.LogInformation("---------------------------------");
      
      return Task.CompletedTask;
    }
  }
}
