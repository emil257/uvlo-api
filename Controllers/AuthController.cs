
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase 
{
    [HttpGet("me")]
    public Results<Ok<String>, ProblemHttpResult> Me()
    {
			string? Name = HttpContext.User.Identity?.Name;

			return TypedResults.Ok(Name);
    }
}
