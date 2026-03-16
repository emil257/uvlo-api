
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using uvlo_api.Data.Models.Database;

[Route("auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
  private readonly SignInManager<AppUserModel> _signInManager;
  private readonly UserManager<AppUserModel> _userManager;
  private readonly ILogger<AuthController> _logger;

  public AuthController(SignInManager<AppUserModel> signInManager, ILogger<AuthController> logger, UserManager<AppUserModel> userManager)
  {
    _signInManager = signInManager;
    _logger = logger;
    _userManager = userManager;
  }

  [HttpGet("me")]
  public async Task<Results<Ok<AppUserModel>, NotFound>> Me()
  {
    string? Name = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    bool isParsed = int.TryParse(Name, out int userId);
    AppUserModel? user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
    if(user == null)
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
