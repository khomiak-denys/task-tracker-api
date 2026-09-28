using FluentValidation;

namespace Tasks.Application.Tasks.GetMy
{
    public class GetMyQueryValidator : AbstractValidator<GetMyQuery>
    {
        public GetMyQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId must not be empty.");

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");

            RuleFor(x => x.Type)
                .Must(t => string.IsNullOrEmpty(t) || t.Equals("created", StringComparison.OrdinalIgnoreCase) || t.Equals("assigned", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Type must be either 'created', 'assigned', or omitted.");
        }
    }
}
