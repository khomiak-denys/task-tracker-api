using FluentValidation;

namespace Workspaces.Application.Workspaces.Create
{
    /// <summary>Validates <see cref="CreateWorkspaceCommand"/> before the handler executes.</summary>
    public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
    {
        /// <summary>Initializes a new instance of <see cref="CreateWorkspaceCommandValidator"/>.</summary>
        public CreateWorkspaceCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.")
                .When(x => x.Description is not null);
        }
    }
}
