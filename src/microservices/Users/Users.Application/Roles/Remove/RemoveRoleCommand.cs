using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Roles.Remove
{
    /// <summary>
    /// Command to remove a role from a user.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <param name="Role">The application role to remove.</param>
    public record RemoveRoleCommand(Guid UserId, AppRole Role) : ICommand<Result>;
}
