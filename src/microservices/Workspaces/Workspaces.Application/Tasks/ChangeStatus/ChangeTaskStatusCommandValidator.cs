using FluentValidation;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;

namespace Workspaces.Application.Tasks.ChangeStatus
{
    /// <summary>Validates <see cref="ChangeTaskStatusCommand"/> before the handler executes.</summary>
    public class ChangeTaskStatusCommandValidator : AbstractValidator<ChangeTaskStatusCommand>
    {
        private static readonly TaskStatus[] AllowedTargetStatuses = [TaskStatus.InProgress, TaskStatus.InReview];

        /// <summary>Initializes a new instance of <see cref="ChangeTaskStatusCommandValidator"/>.</summary>
        public ChangeTaskStatusCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.RequestedById)
                .NotEmpty().WithMessage("RequestedById is required.");

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Invalid task status value.")
                .Must(s => AllowedTargetStatuses.Contains(s))
                .WithMessage("Only transitions to InProgress or InReview are allowed via this endpoint.");
        }
    }
}
