using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.Delete
{
    /// <summary>
    /// Handles <see cref="DeleteUserCommand"/> by delegating to the user service.
    /// </summary>
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            return _userService.DeleteAsync(command.UserId, cancellationToken);
        }
    }
}
