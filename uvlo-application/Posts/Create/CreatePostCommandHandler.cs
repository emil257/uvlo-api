using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using uvlo_application.Abstractions.Auth;
using uvlo_application.Abstractions.Data;
using uvlo_application.Abstractions.Messaging;
using uvlo_application.Abstractions.Repos;
using uvlo_application.Abstractions.Services;
using uvlo_application.Exceptions;
using uvlo_application.View;
using uvlo_core.Entities;
using uvlo_core.Enums;
using uvlo_core.Events;

namespace uvlo_application.Posts.Create
{
  internal sealed class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, PostView>
  {
    private readonly IPostsRepo _postsRepo;
    private readonly IUserContext _userContext;
    private readonly IAppDbContext _appDbContext;
    private readonly IFileUploadService _fileUploadService;
    public CreatePostCommandHandler(IPostsRepo postsRepo, IUserContext userContext, IAppDbContext appDbContext, IFileUploadService fileUploadService)
    {
      _postsRepo = postsRepo;
      _userContext = userContext;
      _appDbContext = appDbContext;
      _fileUploadService = fileUploadService;
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

      if(request.attachments != null)
      {
        foreach (IFormFile file in request.attachments)
        {
          using var stream = file.OpenReadStream();
          string fileUrl = await _fileUploadService.SaveFileAsync(stream, file.FileName, file.ContentType);
        }

      }

      _appDbContext.Posts.Add(post);

      post.Raise(new PostCreatedDomainEvent(post));

      await _appDbContext.SaveChangesAsync(cancellationToken);

      return new PostView(post);
    }
  }
}
