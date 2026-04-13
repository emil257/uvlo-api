using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;

namespace uvlo_application.Posts.GetById
{
  internal sealed class GetChildrenQueryHandler : IQueryHandler<GetByIdQuery, PostView>
  {
    private readonly IPostsRepo _postsRepo;
    public GetChildrenQueryHandler(IPostsRepo postsRepo)
    {
      _postsRepo = postsRepo;
    }
    public async Task<PostView> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
      PostView? post = await _postsRepo.GetPostViewAsync(query.id, cancellationToken);
      if (post == null)
        throw new AppException("Post not found", 404);
      return post;
    }
  }
}
