using uvlo_application.Abstractions.Auth;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;
using uvlo_core.Entities;
using uvlo_core.Enums;

namespace uvlo_application.Posts.Delete
{
  internal sealed class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, PostView>
  {
    private readonly IPostsRepo _postsRepo;
    private readonly IUserContext _userContext;

    public DeletePostCommandHandler(IPostsRepo postsRepo, IUserContext userContext)
    {
      _postsRepo = postsRepo;
      _userContext = userContext;
    }
    public async Task<PostView> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(request.id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      if (post.UserId != _userContext.UserId)
        throw new AppException("Unauthorized", 403);
      post.SoftDelete();
      await _postsRepo.Save(post, cancellationToken);
      return new PostView(post);
    }
  }
}
