using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;

namespace uvlo_application.Posts.GetFeed
{
  internal sealed class GetByIdQueryHandler : IQueryHandler<GetFeedQuery, List<PostView>>
  {
    private readonly IPostsRepo _postsRepo;
    public GetByIdQueryHandler(IPostsRepo postsRepo)
    {
      _postsRepo = postsRepo;
    }
    public async Task<List<PostView>> Handle(GetFeedQuery query, CancellationToken cancellationToken)
    {
      return await _postsRepo.GetPostsViewAsync(null, cancellationToken);
    }
  }
}
