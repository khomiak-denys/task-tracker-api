using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;

namespace Users.Application.Users.GetContactInfo
{
    /// <summary>
    /// Query to retrieve user contact information by ID.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user whose contact information is requested.</param>
    public record GetUserContactInfoQuery(Guid UserId) : IQuery<Result<UserContactInfoResult>>;
}
