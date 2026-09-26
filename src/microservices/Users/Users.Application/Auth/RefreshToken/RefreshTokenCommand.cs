using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Auth.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<Result<AuthTokensResult>>;

    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<AuthTokensResult>>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<Result<AuthTokensResult>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            return _authService.RefreshTokenAsync(command.AccessToken, command.RefreshToken, cancellationToken);
        }
    }
}
