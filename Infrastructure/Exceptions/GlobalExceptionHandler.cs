using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using uvlo_api.Infrastructure.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
  private readonly ILogger<GlobalExceptionHandler> _logger;
  public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
  {
    _logger = logger;
  }
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

    var problemDetails = new ProblemDetails
    {
      Status = StatusCodes.Status500InternalServerError,
      Title = "Server error"
    };
    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

    if (exception is AppException appExeption)
    {
      problemDetails = new ProblemDetails
      {
        Status = appExeption.StatusCode,
        Title = appExeption.Message
      };
      httpContext.Response.StatusCode = appExeption.StatusCode;
    }

    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

    return true;
  }
}
