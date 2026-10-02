using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetContactInfo
{
    /// <summary>
    /// Handles <see cref="GetUserContactInfoQuery"/> by delegating to the user service.
    /// </summary>
    public class GetUserContactInfoQueryHandler : IQueryHandler<GetUserContactInfoQuery, Result<UserContactInfoResult>>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserContactInfoQueryHandler"/> class.
        /// </summary>
        /// <param name="userService">The user service instance.</param>
        public GetUserContactInfoQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <inheritdoc />
        public Task<Result<UserContactInfoResult>> Handle(GetUserContactInfoQuery request, CancellationToken cancellationToken)
        {
            return _userService.GetContactInfoByIdAsync(request.UserId, cancellationToken);
        }
    }
}
