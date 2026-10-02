using FluentValidation;

namespace Users.Application.Users.ChangePassword
{
    /// <summary>
    /// Validator for <see cref="ChangeUserPasswordCommand"/>.
    /// </summary>
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).NotEqual(x => x.CurrentPassword);
        }
    }
}
