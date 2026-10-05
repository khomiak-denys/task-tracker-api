using FluentValidation;

namespace Workspaces.Application.Tasks.GetAll
{
    /// <summary>
    /// Validates <see cref="GetAllTasksQuery"/> before execution.
    /// </summary>
    public class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
    {
        /// <summary>Initializes a new instance of <see cref="GetAllTasksQueryValidator"/>.</summary>
        public GetAllTasksQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
