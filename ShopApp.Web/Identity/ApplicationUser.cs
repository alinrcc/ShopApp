using Microsoft.AspNetCore.Identity;

namespace ShopApp.Web.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
