using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.GetForUser
{
    public record GetForUserQuery(Guid UserId) : IQuery<Result<IReadOnlyList<string>>>;

    public class GetForUserQueryHandler : IQueryHandler<GetForUserQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IRoleService _roleService;

        public GetForUserQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result<IReadOnlyList<string>>> Handle(GetForUserQuery request, CancellationToken cancellationToken)
        {
            return _roleService.GetForUserAsync(request.UserId, cancellationToken);
        }
    }
}
