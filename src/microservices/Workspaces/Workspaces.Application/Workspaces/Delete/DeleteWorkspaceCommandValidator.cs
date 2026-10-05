using FluentValidation;

namespace Workspaces.Application.Workspaces.Delete
{
    /// <summary>Validates <see cref="DeleteWorkspaceCommand"/> before the handler executes.</summary>
    public class DeleteWorkspaceCommandValidator : AbstractValidator<DeleteWorkspaceCommand>
    {
        /// <summary>Initializes a new instance of <see cref="DeleteWorkspaceCommandValidator"/>.</summary>
        public DeleteWorkspaceCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("WorkspaceId is required.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty().WithMessage("RequestingUserId is required.");
        }
    }
}
