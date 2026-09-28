using FluentValidation;

namespace Users.Application.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().Length(3, 50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
