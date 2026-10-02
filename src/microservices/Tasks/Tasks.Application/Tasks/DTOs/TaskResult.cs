using Tasks.Domain.Tasks;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.Application.Tasks.DTOs
{
    /// <summary>
    /// Represents a task summary item for list projections.
    /// </summary>
    /// <param name="Id">The unique identifier of the task.</param>
    /// <param name="Title">The title of the task.</param>
    /// <param name="Description">The optional description of the task.</param>
    /// <param name="Status">The current workflow status of the task.</param>
    /// <param name="Priority">The priority level of the task.</param>
    /// <param name="Deadline">The optional deadline date and time.</param>
    /// <param name="AssigneeId">The unique identifier of the assigned user, if any.</param>
    /// <param name="Tags">The list of tag names associated with the task.</param>
    public record TaskResult(
        Guid Id,
        string Title,
        string? Description,
        TaskStatus Status,
        Priority Priority,
        DateTime? Deadline,
        Guid? AssigneeId,
        IReadOnlyList<string> Tags);
}
