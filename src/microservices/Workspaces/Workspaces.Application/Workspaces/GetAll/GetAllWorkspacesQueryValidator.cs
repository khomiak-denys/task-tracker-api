using FluentValidation;

namespace Workspaces.Application.Workspaces.GetAll
{
    /// <summary>
    /// Validates <see cref="GetAllWorkspacesQuery"/> before execution.
    /// </summary>
    public class GetAllWorkspacesQueryValidator : AbstractValidator<GetAllWorkspacesQuery>
    {
        /// <summary>Initializes a new instance of <see cref="GetAllWorkspacesQueryValidator"/>.</summary>
        public GetAllWorkspacesQueryValidator()
        {
            RuleFor(x => x.RequestingUserId)
                .NotEmpty()
                .WithMessage("RequestingUserId is required.");

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
