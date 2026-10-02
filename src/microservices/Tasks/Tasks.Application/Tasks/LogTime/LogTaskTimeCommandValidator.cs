using FluentValidation;

namespace Tasks.Application.Tasks.LogTime
{
    /// <summary>Validates <see cref="LogTaskTimeCommand"/> before the handler executes.</summary>
    public class LogTaskTimeCommandValidator : AbstractValidator<LogTaskTimeCommand>
    {
        /// <summary>Initializes a new instance of <see cref="LogTaskTimeCommandValidator"/>.</summary>
        public LogTaskTimeCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.MinutesSpent)
                .GreaterThan(0).WithMessage("MinutesSpent must be a positive number.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(x => x.Description is not null);

            RuleFor(x => x.LoggedDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("LoggedDate cannot be in the future.");
        }
    }
}
