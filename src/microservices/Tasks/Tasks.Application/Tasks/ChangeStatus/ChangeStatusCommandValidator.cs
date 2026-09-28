using FluentValidation;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.Application.Tasks.ChangeStatus
{
    /// <summary>Validates <see cref="ChangeStatusCommand"/> before the handler executes.</summary>
    public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
    {
        private static readonly TaskStatus[] AllowedTargetStatuses = [TaskStatus.InProgress, TaskStatus.InReview];

        public ChangeStatusCommandValidator()
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
