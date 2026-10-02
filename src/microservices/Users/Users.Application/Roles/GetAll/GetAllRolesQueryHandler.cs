using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.GetAll
{
    /// <summary>
    /// Handles <see cref="GetAllRolesQuery"/> by delegating to the role service.
    /// </summary>
    public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result<IReadOnlyList<string>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleService.GetAllAsync(cancellationToken);
        }
    }
}
