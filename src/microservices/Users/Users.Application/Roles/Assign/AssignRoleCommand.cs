using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Roles.Assign
{
    /// <summary>
    /// Command to assign a role to a user.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <param name="Role">The application role to assign.</param>
    public record AssignRoleCommand(Guid UserId, AppRole Role) : ICommand<Result>;
}
