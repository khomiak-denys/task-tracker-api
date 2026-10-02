using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;

namespace Users.Application.Users.GetAll
{
    /// <summary>
    /// Query to retrieve all users with pagination.
    /// </summary>
    /// <param name="Page">The page number.</param>
    /// <param name="PageSize">The page size.</param>
    public record GetAllUsersQuery(int Page, int PageSize) : IQuery<Result<PaginationResult<UserResult>>>;
}
