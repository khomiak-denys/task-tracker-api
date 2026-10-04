using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Tasks.Cancel
{
    /// <summary>
    /// Command to cancel a task.
    /// A completed task cannot be cancelled.
    /// </summary>
    /// <param name="TaskId">The task to cancel.</param>
    /// <param name="RequestedById">Id of the authenticated user requesting the cancellation.</param>
    public record CancelTaskCommand(
        Guid TaskId,
        Guid RequestedById) : ICommand<Result>;
}
