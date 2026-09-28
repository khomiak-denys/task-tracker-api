using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Roles.Assign
{
    public record AssignCommand(Guid UserId, AppRole Role) : ICommand<Result>;

    public class AssignCommandHandler : ICommandHandler<AssignCommand, Result>
    {
        private readonly IRoleService _roleService;

        public AssignCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public Task<Result> Handle(AssignCommand command, CancellationToken cancellationToken)
        {
            return _roleService.AssignAsync(command.UserId, command.Role, cancellationToken);
        }
    }
}
