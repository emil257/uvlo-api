
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using uvlo_api.Api.Data.Models;

[Route("auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase 
{
		private readonly SignInManager<AppUserModel> _signInManager;

		public AuthController(SignInManager<AppUserModel> signInManager){
			_signInManager = signInManager;
		}

    [HttpGet("me")]
    public Results<Ok<String>, ProblemHttpResult> Me()
    {
			string? Name = HttpContext.User.Identity?.Name;

			return TypedResults.Ok(Name);
    }
    [HttpPost("logout")]
    public async Task<Results<Ok, ProblemHttpResult>> Logout()
    {
			await _signInManager.SignOutAsync();	

			return TypedResults.Ok();
    }
}
