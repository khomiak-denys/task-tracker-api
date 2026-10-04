using DomainFramework.Results;
using Messaging.Abstractions;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;

namespace Workspaces.Application.Tasks.ChangeStatus
{
    /// <summary>
    /// Command to advance a task through the allowed status transitions:
    /// Todo → InProgress → InReview.
    /// </summary>
    /// <param name="TaskId">The task to transition.</param>
    /// <param name="RequestedById">Id of the user performing the change.</param>
    /// <param name="NewStatus">Desired target status.</param>
    public record ChangeTaskStatusCommand(
        Guid TaskId,
        Guid RequestedById,
        TaskStatus NewStatus) : ICommand<Result>;
}
