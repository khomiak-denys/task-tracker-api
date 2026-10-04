using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspaces.Domain.Tags;

namespace Workspaces.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core entity type configuration for <see cref="Tag"/>.
    /// </summary>
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Tag"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.TaskTags)
                .WithOne(tt => tt.Tag)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
