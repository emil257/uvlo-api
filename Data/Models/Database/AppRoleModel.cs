using Microsoft.AspNetCore.Identity;

namespace uvlo_api.Data.Models.Database
{
    public class AppRoleModel: IdentityRole<int>
    {
        public AppRoleModel(string name) : base(name) { }
    }
}
