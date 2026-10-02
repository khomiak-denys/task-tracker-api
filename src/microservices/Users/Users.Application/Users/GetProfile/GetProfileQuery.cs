using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetProfile
{
    public record GetProfileQuery(Guid TargetUserId, Guid CurrentUserId, bool IsAdmin) : IQuery<Result<UserProfileResult>>;

    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, Result<UserProfileResult>>
    {
        private readonly IUserService _userService;

        public GetProfileQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<UserProfileResult>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsAdmin && request.TargetUserId != request.CurrentUserId)
            {
                return Result<UserProfileResult>.Failure(new ForbiddenError("Users can only access their own profile."));
            }

            return await _userService.GetByIdAsync(request.TargetUserId, cancellationToken);
        }
    }
}
