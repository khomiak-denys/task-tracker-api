using FluentValidation;

namespace Workspaces.Application.Tasks.Cancel
{
    /// <summary>Validates <see cref="CancelTaskCommand"/> before the handler executes.</summary>
    public class CancelTaskCommandValidator : AbstractValidator<CancelTaskCommand>
    {
        /// <summary>Initializes a new instance of <see cref="CancelTaskCommandValidator"/>.</summary>
        public CancelTaskCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.RequestedById)
                .NotEmpty().WithMessage("RequestedById is required.");
        }
    }
}
