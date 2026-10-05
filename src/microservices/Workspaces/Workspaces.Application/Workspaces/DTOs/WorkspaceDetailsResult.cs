namespace Workspaces.Application.Workspaces.DTOs
{
    /// <summary>
    /// Represents detailed workspace information including the list of member user ids.
    /// </summary>
    /// <param name="Id">The unique identifier of the workspace.</param>
    /// <param name="Name">The workspace name.</param>
    /// <param name="OwnerId">The user id of the workspace owner.</param>
    /// <param name="Description">The optional workspace description.</param>
    /// <param name="MemberIds">The list of user ids that are members of the workspace.</param>
    /// <param name="CreatedAt">When the workspace was created.</param>
    /// <param name="UpdatedAt">When the workspace was last updated.</param>
    public record WorkspaceDetailsResult(
        Guid Id,
        string Name,
        Guid OwnerId,
        string? Description,
        IReadOnlyList<Guid> MemberIds,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
