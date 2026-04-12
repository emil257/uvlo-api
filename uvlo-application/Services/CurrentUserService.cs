using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using uvlo_application.Exceptions;

namespace uvlo_application.Services
{
  public class CurrentUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId) ? userId : throw new AppException("User not authenticated", 401);
  }
}
