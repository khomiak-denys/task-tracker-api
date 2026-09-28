using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Tasks;

namespace Tasks.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core entity type configuration for the <see cref="TaskTag"/> join entity.
    /// </summary>
    public class TaskTagConfiguration : IEntityTypeConfiguration<TaskTag>
    {
        /// <summary>
        /// Configures the join entity of type <see cref="TaskTag"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<TaskTag> builder)
        {
            builder.ToTable("TaskTags");

            builder.HasKey(tt => new { tt.TaskId, tt.TagId });
        }
    }
}
