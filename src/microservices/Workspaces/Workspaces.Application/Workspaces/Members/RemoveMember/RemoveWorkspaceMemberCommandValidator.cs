using FluentValidation;

namespace Workspaces.Application.Workspaces.Members.RemoveMember
{
    /// <summary>
    /// Validates <see cref="RemoveWorkspaceMemberCommand"/> before execution.
    /// </summary>
    public class RemoveWorkspaceMemberCommandValidator : AbstractValidator<RemoveWorkspaceMemberCommand>
    {
        /// <summary>Initializes a new instance of <see cref="RemoveWorkspaceMemberCommandValidator"/>.</summary>
        public RemoveWorkspaceMemberCommandValidator()
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
