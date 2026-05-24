using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using uvlo_api.Dto;
using uvlo_application.Abstractions.Auth;
using uvlo_application.Posts.Create;
using uvlo_application.Posts.Delete;
using uvlo_application.Posts.GetById;
using uvlo_application.Posts.GetFeed;
using uvlo_application.Posts.Like;
using uvlo_application.Posts.Update;
using uvlo_application.View;

[Route("posts")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
  private readonly ISender _sender;
  public PostController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost()]
  public async Task<Results<Ok<PostView>, ProblemHttpResult>> CreatePost(
    [FromForm] PostCreateDto postCreateDto
  )
  {
    CreatePostCommand command = new CreatePostCommand(postCreateDto.Content, postCreateDto.ParentId, postCreateDto.Attachments);

    PostView createdPost = await _sender.Send(command, HttpContext.RequestAborted);

    return TypedResults.Ok(createdPost);
  }

  [HttpGet()]
  public async Task<Results<Ok<IEnumerable<PostView>>, NotFound>> GetPosts()
  {
    GetFeedQuery query = new GetFeedQuery();

    IEnumerable<PostView> posts = await _sender.Send(query, HttpContext.RequestAborted);

    return TypedResults.Ok(posts);
  }

  [HttpGet("{id}")]
  public async Task<Results<Ok<PostView>, NotFound>> GetPostById(int id)
  {
    GetByIdQuery query = new GetByIdQuery(id);

    PostView post = await _sender.Send(query, HttpContext.RequestAborted);

    return TypedResults.Ok(post);
  }
  [HttpGet("{id}/children")]
  public async Task<Results<Ok<IEnumerable<PostView>>, NotFound>> GetPostChildrenById(int id)
  {
    GetChildrenQuery query = new GetChildrenQuery(id);

    IEnumerable<PostView> postComments = await _sender.Send(query, HttpContext.RequestAborted);

    return TypedResults.Ok(postComments);
  }
  [HttpDelete("{id}")]
  public async Task<Results<Ok<PostView>, NotFound>> DeletePost(int id)
  {
    DeletePostCommand command = new DeletePostCommand(id);

    PostView post = await _sender.Send(command, HttpContext.RequestAborted);
    
    return TypedResults.Ok(post);
  }
  [HttpPut()]
  public async Task<Results<Ok<PostView>, NotFound>> UpdatePost([FromBody] PostUpdateDto postUpdateDto)
  {
    UpdatePostCommand command = new UpdatePostCommand(postUpdateDto.Id,  postUpdateDto.Content);

    PostView post = await _sender.Send(command, HttpContext.RequestAborted);
    
    return TypedResults.Ok(post);
  }
  [HttpPost("{id}/like")]
  public async Task<Results<Ok<PostView>, NotFound>> LikePost(int id)
  {
    LikePostCommand command = new LikePostCommand(id);

    PostView post = await _sender.Send(command, HttpContext.RequestAborted);

    return TypedResults.Ok(post);
  }
}



