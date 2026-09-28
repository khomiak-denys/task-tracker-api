using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.Delete
{
    public record DeleteCommand(Guid UserId) : ICommand<Result>;

    public class DeleteCommandHandler : ICommandHandler<DeleteCommand, Result>
    {
        private readonly IUserService _userService;

        public DeleteCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            return _userService.DeleteAsync(command.UserId, cancellationToken);
        }
    }
}
