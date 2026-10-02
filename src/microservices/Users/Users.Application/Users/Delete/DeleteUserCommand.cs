using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Users.Delete
{
    /// <summary>
    /// Command to delete a user by ID.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user to delete.</param>
    public record DeleteUserCommand(Guid UserId) : ICommand<Result>;
}
