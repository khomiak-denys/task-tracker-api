using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Tasks;

namespace Tasks.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core entity type configuration for <see cref="TimeLog"/>.
    /// </summary>
    public class TimeLogConfiguration : IEntityTypeConfiguration<TimeLog>
    {
        /// <summary>
        /// Configures the entity of type <see cref="TimeLog"/>.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<TimeLog> builder)
        {
            builder.ToTable("TimeLogs");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.TaskId).IsRequired();
            builder.Property(l => l.UserId).IsRequired();

            builder.Property(l => l.MinutesSpent).IsRequired();

            builder.Property(l => l.Description)
                .HasMaxLength(500);

            builder.Property(l => l.LoggedDate).IsRequired();
            builder.Property(l => l.CreatedAt).IsRequired();
            builder.Property(l => l.UpdatedAt);
        }
    }
}
