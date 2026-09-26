using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : ICommand<Result>;

    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            return _userService.ChangePasswordAsync(command.UserId, command.CurrentPassword, command.NewPassword, cancellationToken);
        }
    }
}
