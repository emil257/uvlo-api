using Microsoft.AspNetCore.Identity;

public class AppRoleModel : IdentityRole<int>
{
  public AppRoleModel(string name) : base(name) { }
}
