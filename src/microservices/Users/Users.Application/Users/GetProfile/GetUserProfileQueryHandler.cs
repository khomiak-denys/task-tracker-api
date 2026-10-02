using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetProfile
{
    /// <summary>
    /// Handles <see cref="GetUserProfileQuery"/> by validating permissions and retrieving the user profile.
    /// </summary>
    public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, Result<UserProfileResult>>
    {
        private readonly IUserService _userService;

        public GetUserProfileQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<UserProfileResult>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            if (request.CurrentUserId.HasValue && !request.IsAdmin && request.TargetUserId != request.CurrentUserId.Value)
            {
                return Result<UserProfileResult>.Failure(new ForbiddenError("Users can only access their own profile."));
            }

            return await _userService.GetByIdAsync(request.TargetUserId, cancellationToken);
        }
    }
}
