using DomainFramework.Results;
using Messaging.Abstractions;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.Application.Auth.Register
{
    public record RegisterCommand(string Email, string UserName, string Password) : ICommand<Result<AuthTokensResult>>;

    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<AuthTokensResult>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<Result<AuthTokensResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return _authService.RegisterAsync(command.Email, command.UserName, command.Password, cancellationToken);
        }
    }
}
