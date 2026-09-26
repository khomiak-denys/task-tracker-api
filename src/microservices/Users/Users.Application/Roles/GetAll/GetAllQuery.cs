using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.GetAll
{
    public record GetAllQuery() : IQuery<Result<IReadOnlyList<string>>>;

    public class GetAllQueryHandler : IQueryHandler<GetAllQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IRoleService _roleService;

        public GetAllQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result<IReadOnlyList<string>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return _roleService.GetAllAsync(cancellationToken);
        }
    }
}
