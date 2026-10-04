namespace Workspaces.Application.Tasks.DTOs
{
    public record TimeLogResult(
        Guid Id,
        Guid UserId,
        int MinutesSpent,
        string? Description,
        DateOnly LoggedDate,
        DateTime CreatedAt);
}
