using System.Security.Claims;
using uvlo_api.Infrastructure.Exceptions;

namespace uvlo_api.Services
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
