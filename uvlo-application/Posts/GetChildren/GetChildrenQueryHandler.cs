using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.Posts.GetById;
using uvlo_application.View;

namespace uvlo_application.Posts.GetChildren
{
  internal sealed class GetChildrenQueryHandler : IQueryHandler<GetChildrenQuery, List<PostView>>
  {
    private readonly IPostsRepo _postsRepo;
    public GetChildrenQueryHandler(IPostsRepo postsRepo)
    {
      _postsRepo = postsRepo;
    }
    public async Task<List<PostView>> Handle(GetChildrenQuery query, CancellationToken cancellationToken)
    {
      return await _postsRepo.GetPostsViewAsync(query.parentId, cancellationToken);
    }
  }
}
