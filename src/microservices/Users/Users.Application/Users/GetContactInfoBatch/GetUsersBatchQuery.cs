using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;

namespace Users.Application.Users.GetContactInfoBatch
{
    /// <summary>
    /// Query to retrieve paginated contact information for a batch of user identifiers.
    /// </summary>
    /// <param name="UserIds">The collection of user unique identifiers to look up.</param>
    /// <param name="Page">The page number (1-based).</param>
    /// <param name="PageSize">The page size.</param>
    public record GetUsersBatchQuery(
        IReadOnlyList<Guid> UserIds,
        int Page = 1,
        int PageSize = 10) : IQuery<Result<PaginationResult<UserContactInfoResult>>>;
}
