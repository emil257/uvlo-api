using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using uvlo_api.Data.Models.Dto;
using uvlo_api.Data.Models.View;
using uvlo_api.Services;

[Route("posts")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
  private readonly PostService _postService;
  private readonly CurrentUserService _currentUserService;
  public PostController(PostService postService, CurrentUserService currentUserService)
  {
    _postService = postService;
    _currentUserService = currentUserService;
  }

  [HttpPost()]
  public async Task<Results<Ok<PostView>, ProblemHttpResult>> CreatePost(
    [FromBody] PostCreateDto postCreateDto
  )
  {
    PostView createdPost = await _postService.CreatePostAsync(postCreateDto.Content, postCreateDto.ParentId, _currentUserService.UserId, HttpContext.RequestAborted);

    return TypedResults.Ok(createdPost);
  }

  [HttpGet()]
  public async Task<Results<Ok<IEnumerable<PostView>>, NotFound>> GetPosts()
  {
    IEnumerable<PostView> posts = await _postService.GetPostsAsync(HttpContext.RequestAborted);

    return TypedResults.Ok(posts);
  }

  [HttpGet("{id}")]
  public async Task<Results<Ok<PostView>, NotFound>> GetPostById(int id)
  {
    PostView post = await _postService.GetPostById(id, HttpContext.RequestAborted);
    
    return TypedResults.Ok(post);
  }
  [HttpGet("{id}/children")]
  public async Task<Results<Ok<IEnumerable<PostView>>, NotFound>> GetPostChildrenById(int id)
  {
    IEnumerable<PostView> postComments = await _postService.GetPostChildrenById(id, HttpContext.RequestAborted);

    return TypedResults.Ok(postComments);
  }
  [HttpDelete("{id}")]
  public async Task<Results<Ok<PostView>, NotFound>> DeletePost(int id)
  {
    PostView post = await _postService.DeletePost(id, _currentUserService.UserId, HttpContext.RequestAborted);
    
    return TypedResults.Ok(post);
  }
  [HttpPut()]
  public async Task<Results<Ok<PostView>, NotFound>> UpdatePost([FromBody] PostUpdateDto postUpdateDto)
  {
    PostView post = await _postService.UpdatePost(postUpdateDto.Id, postUpdateDto.Title, postUpdateDto.Content, _currentUserService.UserId, HttpContext.RequestAborted);
    
    return TypedResults.Ok(post);
  }
}



