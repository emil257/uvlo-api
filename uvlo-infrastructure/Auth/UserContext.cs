using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using uvlo_application.Exceptions;
using uvlo_application.Abstractions.Auth;

namespace uvlo_infrastructure.Auth
{
  public class UserContext : IUserContext
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId) ? userId : throw new AppException("User not authenticated", 401);
  }
}
