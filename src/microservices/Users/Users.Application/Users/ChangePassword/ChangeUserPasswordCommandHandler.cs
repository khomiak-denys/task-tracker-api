using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.ChangePassword
{
    /// <summary>
    /// Handles <see cref="ChangeUserPasswordCommand"/> by delegating to the user service.
    /// </summary>
    public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ChangeUserPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        {
            return _userService.ChangePasswordAsync(command.UserId, command.CurrentPassword, command.NewPassword, cancellationToken);
        }
    }
}
