using DomainFramework.Results;
using Messaging.Abstractions;
using Priority = Tasks.Domain.Tasks.Priority;

namespace Tasks.Application.Tasks.Update
{
    /// <summary>
    /// Command to edit an existing task's editable fields:
    /// title, description, priority, deadline, and tags.
    /// </summary>
    /// <param name="TaskId">The id of the task to update.</param>
    /// <param name="RequestedById">Id of the authenticated user performing the edit.</param>
    /// <param name="Title">New task title.</param>
    /// <param name="Description">New optional description.</param>
    /// <param name="Priority">New priority level.</param>
    /// <param name="Deadline">New optional deadline.</param>
    /// <param name="TagNames">Updated complete set of tag names.</param>
    public record UpdateTaskCommand(
        Guid TaskId,
        Guid RequestedById,
        string Title,
        string? Description,
        Priority Priority,
        DateTime? Deadline,
        IEnumerable<string> TagNames) : ICommand<Result>;
}
