using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetProfile
{
    public record GetProfileQuery(Guid UserId) : IQuery<Result<UserProfileResult>>;

    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, Result<UserProfileResult>>
    {
        private readonly IUserService _userService;

        public GetProfileQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<UserProfileResult>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return _userService.GetByIdAsync(request.UserId, cancellationToken);
        }
    }
}
