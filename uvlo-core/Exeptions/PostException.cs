namespace uvlo_core.Exeptions
{
  public class PostException : Exception
  {
    public int StatusCode { get; set; }
    public PostException(string message, int statusCode = 500) : base(message)
    {
      StatusCode = statusCode;
    }
  }
}
