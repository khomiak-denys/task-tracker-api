using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.Interfaces;

namespace Users.Application.Auth.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(Guid UserId) : ICommand<Result>;

    public class RevokeRefreshTokenCommandHandler : ICommandHandler<RevokeRefreshTokenCommand, Result>
    {
        private readonly IAuthService _authService;

        public RevokeRefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<Result> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            return _authService.RevokeRefreshTokenAsync(command.UserId, cancellationToken);
        }
    }
}
