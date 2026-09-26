using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.UpdateProfile
{
    public record UpdateProfileCommand(Guid UserId, string? FullName, string? UserName) : ICommand<Result>;

    public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, Result>
    {
        private readonly IUserService _userService;

        public UpdateProfileCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            return _userService.UpdateProfileAsync(command.UserId, command.FullName, command.UserName, cancellationToken);
        }
    }
}
