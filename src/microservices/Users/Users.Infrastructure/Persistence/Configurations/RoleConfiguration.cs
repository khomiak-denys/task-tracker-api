using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Application.Roles;

namespace Users.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public static readonly Guid AdminRoleId = new("a1111111-1111-1111-1111-111111111111");
        public static readonly Guid ManagerRoleId = new("b2222222-2222-2222-2222-222222222222");
        public static readonly Guid UserRoleId = new("c3333333-3333-3333-3333-333333333333");

        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityRole<Guid>
                {
                    Id = AdminRoleId,
                    Name = nameof(AppRole.Admin),
                    NormalizedName = nameof(AppRole.Admin).ToUpperInvariant(),
                    ConcurrencyStamp = "a1111111-1111-1111-1111-111111111111"
                },
                new IdentityRole<Guid>
                {
                    Id = ManagerRoleId,
                    Name = nameof(AppRole.Manager),
                    NormalizedName = nameof(AppRole.Manager).ToUpperInvariant(),
                    ConcurrencyStamp = "b2222222-2222-2222-2222-222222222222"
                },
                new IdentityRole<Guid>
                {
                    Id = UserRoleId,
                    Name = nameof(AppRole.User),
                    NormalizedName = nameof(AppRole.User).ToUpperInvariant(),
                    ConcurrencyStamp = "c3333333-3333-3333-3333-333333333333"
                }
            );
        }
    }
}
