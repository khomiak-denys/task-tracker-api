using FluentValidation;

namespace Workspaces.Application.Workspaces.Update
{
    /// <summary>Validates <see cref="UpdateWorkspaceCommand"/> before the handler executes.</summary>
    public class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
    {
        /// <summary>Initializes a new instance of <see cref="UpdateWorkspaceCommandValidator"/>.</summary>
        public UpdateWorkspaceCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("WorkspaceId is required.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty().WithMessage("RequestingUserId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.")
                .When(x => x.Description is not null);
        }
    }
}
