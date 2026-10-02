using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Users.UpdateProfile
{
    /// <summary>
    /// Command to update a user's profile information.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user to update.</param>
    /// <param name="FullName">The updated full name of the user.</param>
    /// <param name="UserName">The updated username of the user.</param>
    public record UpdateUserProfileCommand(Guid UserId, string? FullName, string? UserName) : ICommand<Result>;
}
