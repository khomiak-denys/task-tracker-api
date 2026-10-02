using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetAll
{
    /// <summary>
    /// Handles <see cref="GetAllUsersQuery"/> by delegating to the user service.
    /// </summary>
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Result<PaginationResult<UserResult>>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<PaginationResult<UserResult>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return _userService.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
