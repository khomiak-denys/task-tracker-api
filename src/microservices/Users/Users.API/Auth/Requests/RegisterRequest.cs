using Users.Application.Auth.Register;

namespace Users.API.Auth.Requests
{
    public record RegisterRequest(string Email, string UserName, string Password)
    {
        public RegisterCommand ToCommand()
        {
            return new RegisterCommand(Email, UserName, Password);
        }
    }
}
