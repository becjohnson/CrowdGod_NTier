using Microsoft.AspNetCore.Identity;

namespace CrowdGod.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserId { get; set; }
    }
}
