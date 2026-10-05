using FluentValidation;

namespace Workspaces.Application.Workspaces.Members.AddMember
{
    /// <summary>
    /// Validates <see cref="AddWorkspaceMemberCommand"/> before execution.
    /// </summary>
    public class AddWorkspaceMemberCommandValidator : AbstractValidator<AddWorkspaceMemberCommand>
    {
        /// <summary>Initializes a new instance of <see cref="AddWorkspaceMemberCommandValidator"/>.</summary>
        public AddWorkspaceMemberCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty()
                .WithMessage("WorkspaceId is required.");

            RuleFor(x => x.MemberUserId)
                .NotEmpty()
                .WithMessage("MemberUserId is required.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty()
                .WithMessage("RequestingUserId is required.");
        }
    }
}
