using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Users.UpdateProfile
{
    /// <summary>
    /// Handles <see cref="UpdateUserProfileCommand"/> by delegating to the user service.
    /// </summary>
    public class UpdateUserProfileCommandHandler : ICommandHandler<UpdateUserProfileCommand, Result>
    {
        private readonly IUserService _userService;

        public UpdateUserProfileCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
        {
            return _userService.UpdateProfileAsync(command.UserId, command.FullName, command.UserName, cancellationToken);
        }
    }
}
