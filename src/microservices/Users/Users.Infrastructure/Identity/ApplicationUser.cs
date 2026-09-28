using Microsoft.AspNetCore.Identity;

namespace Users.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
    }
}
