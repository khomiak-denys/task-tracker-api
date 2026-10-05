namespace Workspaces.Application.Workspaces.DTOs
{
    /// <summary>
    /// Represents a workspace summary item for list projections.
    /// </summary>
    /// <param name="Id">The unique identifier of the workspace.</param>
    /// <param name="Name">The workspace name.</param>
    /// <param name="OwnerId">The user id of the workspace owner.</param>
    /// <param name="Description">The optional workspace description.</param>
    /// <param name="MemberCount">The number of members in the workspace.</param>
    /// <param name="TaskCount">The number of tasks in the workspace.</param>
    /// <param name="CreatedAt">When the workspace was created.</param>
    /// <param name="UpdatedAt">When the workspace was last updated.</param>
    public record WorkspaceResult(
        Guid Id,
        string Name,
        Guid OwnerId,
        string? Description,
        int MemberCount,
        int TaskCount,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
