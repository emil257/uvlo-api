using Microsoft.EntityFrameworkCore;
using System.Data;
using uvlo_application.Abstractions.Auth;
using uvlo_application.Abstractions.Data;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Exceptions;
using uvlo_application.View;
using uvlo_core.Entities;
using uvlo_core.Events;

namespace uvlo_application.Posts.Like
{
  internal sealed class LikePostCommandHandler : ICommandHandler<LikePostCommand, PostView>
  {
    private readonly IUserContext _userContext;
    private readonly IAppDbContext _appDbContext;
    private readonly IPostsRepo _postsRepo;
    public LikePostCommandHandler(IUserContext userContext, IAppDbContext appDbContext, IPostsRepo postsRepo)
    {
      _userContext = userContext;
      _appDbContext = appDbContext;
      _postsRepo = postsRepo;
    } 
    public async Task<PostView> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
      Post? post = await _postsRepo.GetPostAsync(request.PostId, cancellationToken);

      if (post == null)
        throw new AppException("Post not found", 404);

      PostLike? existingLike = await _appDbContext.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == request.PostId && pl.UserId == _userContext.UserId, cancellationToken);

      if(existingLike is null)
      {
        _appDbContext.PostLikes.Add(new PostLike(request.PostId, _userContext.UserId));
        post.LikesCount++;
        post.Raise(new PostLikedDomainEvent(post, _userContext.UserId));
      }
      else
      {
        _appDbContext.PostLikes.Remove(existingLike);
        post.LikesCount--;
      }


      await _appDbContext.SaveChangesAsync(cancellationToken);

      return new PostView(post);
    }
  }
}
