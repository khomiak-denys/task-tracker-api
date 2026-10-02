using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Roles.GetForUser
{
    /// <summary>
    /// Query to retrieve roles assigned to a user.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    public record GetUserRolesQuery(Guid UserId) : IQuery<Result<IReadOnlyList<string>>>;
}
