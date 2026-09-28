using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Users.GetAll
{
    public record GetAllQuery(int Page, int PageSize) : IQuery<Result<PaginationResult<UserResult>>>;

    public class GetAllQueryHandler : IQueryHandler<GetAllQuery, Result<PaginationResult<UserResult>>>
    {
        private readonly IUserService _userService;

        public GetAllQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<PaginationResult<UserResult>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return _userService.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
