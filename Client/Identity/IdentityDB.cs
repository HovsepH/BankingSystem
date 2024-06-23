using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Client.Identity
{
    public class IdentityDB:IdentityDbContext<ApplicationUser>
    {
        public IdentityDB(DbContextOptions<IdentityDB>  options):base(options)
        {
            
        }
    }
}
