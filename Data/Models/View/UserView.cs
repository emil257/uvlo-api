using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data.Models.View
{
  public class UserView
  {
    public int Id { get; set; }
    public string? UserName { get; set; }
    public UserView() { }
    public UserView(AppUserModel user)
    {
      Id = user.Id;
      UserName = user.UserName;
    }
  }
  public class UserViewMe : UserView
  {
    public string? Email { get; set; }
    public UserViewMe() { }
    public UserViewMe(AppUserModel user) : base(user)
    {
      Email = user.Email;
    }
  }
}
