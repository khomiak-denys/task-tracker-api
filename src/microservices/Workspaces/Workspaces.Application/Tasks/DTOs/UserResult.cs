namespace Workspaces.Application.Tasks.DTOs
{
    /// <summary>
    /// Represents the user summary returned from the Users microservice.
    /// </summary>
    /// <param name="Id">The unique identifier of the user.</param>
    /// <param name="Email">The email address of the user.</param>
    /// <param name="UserName">The username of the user.</param>
    /// <param name="FullName">The full name of the user, if available.</param>
    public record UserResult(
        Guid Id,
        string Email,
        string UserName,
        string? FullName);
}
