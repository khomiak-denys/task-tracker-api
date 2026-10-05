using FluentValidation;

namespace Users.Application.Users.GetContactInfoBatch
{
    /// <summary>
    /// Validates <see cref="GetUsersBatchQuery"/> before execution.
    /// </summary>
    public class GetUsersBatchQueryValidator : AbstractValidator<GetUsersBatchQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersBatchQueryValidator"/> class.
        /// </summary>
        public GetUsersBatchQueryValidator()
        {
            RuleFor(x => x.UserIds)
                .NotNull()
                .WithMessage("UserIds collection cannot be null.");

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
