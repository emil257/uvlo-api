using Microsoft.AspNetCore.Identity;

namespace uvlo_api.Api.Data.Models
{
    public class AppRoleModel: IdentityRole<int>
    {
        public AppRoleModel(string name) : base(name) { }
    }
}
