using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;

namespace Users.Application.Users.GetProfile
{
    /// <summary>
    /// Query to retrieve a user profile by ID.
    /// </summary>
    /// <param name="TargetUserId">The unique identifier of the user whose profile is requested.</param>
    /// <param name="CurrentUserId">The optional unique identifier of the requesting user.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrative privileges.</param>
    public record GetUserProfileQuery(
        Guid TargetUserId,
        Guid? CurrentUserId = null,
        bool IsAdmin = false) : IQuery<Result<UserProfileResult>>;
}
