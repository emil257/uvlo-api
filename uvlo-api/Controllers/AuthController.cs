
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uvlo_application.View;
using uvlo_application.Abstractions.Auth;
using uvlo_core.Entities;

[Route("auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
  private readonly SignInManager<AppUserModel> _signInManager;
  private readonly UserManager<AppUserModel> _userManager;
  private readonly ILogger<AuthController> _logger;
  private readonly IUserContext _userContext;

  public AuthController(SignInManager<AppUserModel> signInManager, ILogger<AuthController> logger, UserManager<AppUserModel> userManager, IUserContext userContext)
  {
    _signInManager = signInManager;
    _logger = logger;
    _userManager = userManager;
    _userContext = userContext;
  }

  [HttpGet("me")]
  public async Task<Results<Ok<UserViewMe>, NotFound>> Me()
  {
    UserViewMe? user = await _userManager.Users.Where(u => u.Id == _userContext.UserId).Select(u => new UserViewMe(u.Id, u.UserName, u.Email)).FirstOrDefaultAsync();
    if (user == null)
    {
      return TypedResults.NotFound();
    }
    return TypedResults.Ok(user);
  }
  [HttpPost("logout")]
  public async Task<Results<Ok, ProblemHttpResult>> Logout()
  {
    await _signInManager.SignOutAsync();

    return TypedResults.Ok();
  }
}
