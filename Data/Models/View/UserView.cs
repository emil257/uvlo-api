using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data.Models.View
{
  public class UserView
  {
    public int Id { get; set; }
    public string? UserName { get; set; }
    public UserView() { }
    public UserView(int id, string? userName)
    {
      Id = id;
      UserName = userName;
    }
  }
  public class UserViewMe : UserView
  {
    public string? Email { get; set; }
    public UserViewMe() { }
    public UserViewMe(int id, string userName, string email) : base(id, userName)
    {
      Email = email;
    }
  }
}
