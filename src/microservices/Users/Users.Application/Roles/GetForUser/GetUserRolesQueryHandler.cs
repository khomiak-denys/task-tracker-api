using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.GetForUser
{
    /// <summary>
    /// Handles <see cref="GetUserRolesQuery"/> by delegating to the role service.
    /// </summary>
    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IRoleService _roleService;

        public GetUserRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result<IReadOnlyList<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleService.GetForUserAsync(request.UserId, cancellationToken);
        }
    }
}
