using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.Remove
{
    /// <summary>
    /// Handles <see cref="RemoveRoleCommand"/> by delegating to the role service.
    /// </summary>
    public class RemoveRoleCommandHandler : ICommandHandler<RemoveRoleCommand, Result>
    {
        private readonly IRoleService _roleService;

        public RemoveRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result> Handle(RemoveRoleCommand command, CancellationToken cancellationToken)
        {
            return _roleService.RemoveAsync(command.UserId, command.Role, cancellationToken);
        }
    }
}
