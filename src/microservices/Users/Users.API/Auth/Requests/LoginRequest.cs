using Users.Application.Auth.Login;

namespace Users.API.Auth.Requests
{
    public record LoginRequest(string Email, string Password)
    {
        public LoginCommand ToCommand()
        {
            return new LoginCommand(Email, Password);
        }
    }
}
