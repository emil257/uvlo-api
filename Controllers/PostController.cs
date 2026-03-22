using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using uvlo_api.Data.Models.Database;

[Route("posts")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
  private readonly IPostsRepo _postsRepo;
  public PostController(IPostsRepo postsRepo)
  {
    _postsRepo = postsRepo;
  }

  [HttpPost()]
  public async Task<Results<Ok<Post>, ProblemHttpResult>> CreatePost(
    [FromBody] PostCreateDto postCreateDto
  )
  {
    int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int UserId);

    Post createdPost = await _postsRepo.CreatePostAsync(postCreateDto, UserId, HttpContext.RequestAborted);

    return TypedResults.Ok(createdPost);
  }

  [HttpGet()]
  public async Task<Results<Ok<IEnumerable<Post>>, NotFound>> GetPosts()
  {
    IEnumerable<Post> posts = await _postsRepo.GetPostsAsync(HttpContext.RequestAborted);
    return TypedResults.Ok(posts);
  }

  [HttpGet("{id}")]
  public async Task<Results<Ok<Post>, NotFound>> GetPostById(int id)
  {
    Post? post = await _postsRepo.GetPostAsync(id, HttpContext.RequestAborted);
    if (post == null)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok(post);
  }
  [HttpDelete("{id}")]
  public async Task<Results<Ok, NotFound>> DeletePost(int id)
  {
    int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int UserId);

    int post = await _postsRepo.DeletePostAsync(id, UserId, HttpContext.RequestAborted);
    if (post == 0)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok();
  }
  [HttpPut()]
  public async Task<Results<Ok, NotFound>> UpdatePost([FromBody] PostUpdateDto postUpdateDto)
  {
    int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int UserId);

    int post = await _postsRepo.UpdatePostAsync(postUpdateDto, UserId, HttpContext.RequestAborted);
    if (post == 0)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok();
  }
  [HttpPost("comment")]
  public async Task<Results<Ok<PostComment>, NotFound>> CreatePostComment([FromBody] PostCommentCreateDto postCommentCreateDto)
  {
    int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int UserId);
    PostComment? createdComment = await _postsRepo.CreatePostCommentAsync(postCommentCreateDto, UserId, HttpContext.RequestAborted);
    if (createdComment == null)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok(createdComment);
  }
}



