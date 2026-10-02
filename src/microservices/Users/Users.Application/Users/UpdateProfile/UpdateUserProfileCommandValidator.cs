using FluentValidation;

namespace Users.Application.Users.UpdateProfile
{
    /// <summary>
    /// Validator for <see cref="UpdateUserProfileCommand"/>.
    /// </summary>
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
