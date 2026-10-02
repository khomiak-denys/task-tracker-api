using DomainFramework.Results;
using Messaging.Abstractions;

namespace Users.Application.Roles.GetAll
{
    /// <summary>
    /// Query to retrieve all available role names.
    /// </summary>
    public record GetAllRolesQuery() : IQuery<Result<IReadOnlyList<string>>>;
}
