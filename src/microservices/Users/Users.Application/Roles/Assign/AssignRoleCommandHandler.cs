using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.Assign
{
    /// <summary>
    /// Handles <see cref="AssignRoleCommand"/> by delegating to the role service.
    /// </summary>
    public class AssignRoleCommandHandler : ICommandHandler<AssignRoleCommand, Result>
    {
        private readonly IRoleService _roleService;

        public AssignRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
        {
            return _roleService.AssignAsync(command.UserId, command.Role, cancellationToken);
        }
    }
}
