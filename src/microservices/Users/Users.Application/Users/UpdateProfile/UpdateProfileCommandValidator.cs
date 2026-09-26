using FluentValidation;

namespace Users.Application.Users.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.UserName).Length(3, 50).When(x => !string.IsNullOrEmpty(x.UserName));
        }
    }
}
