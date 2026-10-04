using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core entity type configuration for <see cref="WorkspaceMember"/>.
    /// </summary>
    public class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember>
    {
        /// <summary>
        /// Configures the entity of type <see cref="WorkspaceMember"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
        {
            builder.ToTable("WorkspaceMembers");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.WorkspaceId)
                .IsRequired();

            builder.Property(m => m.UserId)
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .IsRequired();

            builder.Property(m => m.UpdatedAt);

            builder.HasIndex(m => new { m.WorkspaceId, m.UserId })
                .IsUnique();
        }
    }
}
