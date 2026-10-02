using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Users.ChangePassword
{
    /// <summary>
    /// Command to change a user's password.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <param name="CurrentPassword">The user's current password.</param>
    /// <param name="NewPassword">The user's new password.</param>
    public record ChangeUserPasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : ICommand<Result>;
}
