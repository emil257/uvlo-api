using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using uvlo_api.Infrastructure.Exceptions;

public class AppExceptionHandler : IExceptionHandler
{
  private readonly ILogger<AppExceptionHandler> _logger;
  public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
  {
    _logger = logger;
  }
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    if (exception is not AppException appExeption)
    {
      return false;
    }

    _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

    var problemDetails = new ProblemDetails
    {
      Status = appExeption.StatusCode,
      Title = appExeption.Message
    };
    httpContext.Response.StatusCode = appExeption.StatusCode;

    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

    return true;
  }
}
