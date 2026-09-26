using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.Remove
{
    public record RemoveCommand(Guid UserId, AppRole Role) : ICommand<Result>;

    public class RemoveCommandHandler : ICommandHandler<RemoveCommand, Result>
    {
        private readonly IRoleService _roleService;

        public RemoveCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result> Handle(RemoveCommand command, CancellationToken cancellationToken)
        {
            return _roleService.RemoveAsync(command.UserId, command.Role, cancellationToken);
        }
    }
}
