using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using uvlo_core.Exeptions;

public class PostExceptionHandler : IExceptionHandler
{
  private readonly ILogger<PostExceptionHandler> _logger;
  public PostExceptionHandler(ILogger<PostExceptionHandler> logger)
  {
    _logger = logger;
  }
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    if (exception is not PostException appExeption)
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
