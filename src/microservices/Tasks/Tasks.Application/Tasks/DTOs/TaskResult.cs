using Tasks.Domain.Tasks;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.Application.Tasks.DTOs
{
    public record TaskResult(
        Guid Id,
        string Title,
        string? Description,
        TaskStatus Status,
        Priority Priority,
        DateTime? Deadline,
        Guid? AssigneeId,
        Guid CreatedById,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        IReadOnlyList<string> Tags);
}
