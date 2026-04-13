using uvlo_application.Abstractions.Auth;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;
using uvlo_core.Entities;

namespace uvlo_application.Posts.Update
{
  internal sealed class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, PostView>
  {
    private readonly IPostsRepo _postsRepo;
    private readonly IUserContext _userContext;

    public UpdatePostCommandHandler(IPostsRepo postsRepo, IUserContext userContext)
    {
      _postsRepo = postsRepo;
      _userContext = userContext;
    }
    public async Task<PostView> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(request.id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      if (post.UserId != _userContext.UserId)
        throw new AppException("Unauthorized", 403);
      post.Content = request.content;
      await _postsRepo.Save(post, cancellationToken);
      return new PostView(post);
    }
  }
}