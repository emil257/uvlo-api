namespace uvlo_application.Exceptions
{
  public class AppException : Exception
  {
    public int StatusCode { get; set; }
    public AppException(string message, int statusCode = 500) : base(message)
    {
      StatusCode = statusCode;
    }
  }
}
