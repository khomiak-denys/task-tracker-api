using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Workspaces.Create
{
    /// <summary>
    /// Command to create a new workspace.
    /// </summary>
    /// <param name="Name">Non-empty workspace name (max 200 chars).</param>
    /// <param name="Description">Optional description (max 2000 chars).</param>
    /// <param name="OwnerId">Id of the authenticated user creating the workspace.</param>
    public record CreateWorkspaceCommand(
        string Name,
        string? Description,
        Guid OwnerId) : ICommand<Result<Guid>>;
}
