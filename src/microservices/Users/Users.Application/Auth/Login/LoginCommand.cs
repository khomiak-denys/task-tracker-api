using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Auth.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<Result<AuthTokensResult>>;

    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<AuthTokensResult>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<Result<AuthTokensResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return _authService.LoginAsync(command.Email, command.Password, cancellationToken);
        }
    }
}
