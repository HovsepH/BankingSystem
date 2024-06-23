using Microsoft.AspNetCore.Identity;

namespace Client.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

       
    }
}
