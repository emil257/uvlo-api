using uvlo_application.Abstractions.Auth;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;
using uvlo_core.Entities;
using uvlo_core.Enums;

namespace uvlo_application.Posts.Create
{
  internal sealed class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, PostView>
  {
    private readonly IPostsRepo _postsRepo;
    private readonly IUserContext _userContext;

    public CreatePostCommandHandler(IPostsRepo postsRepo, IUserContext userContext)
    {
      _postsRepo = postsRepo;
      _userContext = userContext;
    }
    public async Task<PostView> Handle(CreatePostCommand request, CancellationToken cancellationToken)
     {
      if (request.parentId != null)
      {
        Post? parentPost = await _postsRepo.GetPostAsync(request.parentId.Value, cancellationToken);
        if (parentPost == null)
          throw new AppException("Parent post not found", 404);
      }

      Post post = new Post(request.content, _userContext.UserId, request.parentId == null ? PostType.Post : PostType.Comment, request.parentId);

      return new PostView(await _postsRepo.Save(post, cancellationToken));
    }
  }
}
