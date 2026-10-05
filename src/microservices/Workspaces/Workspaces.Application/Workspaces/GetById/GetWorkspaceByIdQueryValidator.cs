using FluentValidation;

namespace Workspaces.Application.Workspaces.GetById
{
    /// <summary>
    /// Validates <see cref="GetWorkspaceByIdQuery"/> before execution.
    /// </summary>
    public class GetWorkspaceByIdQueryValidator : AbstractValidator<GetWorkspaceByIdQuery>
    {
        /// <summary>Initializes a new instance of <see cref="GetWorkspaceByIdQueryValidator"/>.</summary>
        public GetWorkspaceByIdQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty()
                .WithMessage("WorkspaceId is required.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty()
                .WithMessage("RequestingUserId is required.");
        }
    }
}
