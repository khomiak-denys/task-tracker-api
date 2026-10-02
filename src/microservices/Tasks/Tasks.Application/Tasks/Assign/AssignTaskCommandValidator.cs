using FluentValidation;

namespace Tasks.Application.Tasks.Assign
{
    /// <summary>
    /// Validates <see cref="AssignTaskCommand"/> before the handler executes.
    /// </summary>
    public class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
    {
        /// <summary>
        /// Initializes validation rules for assigning a task.
        /// </summary>
        public AssignTaskCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty()
                .WithMessage("TaskId must not be empty.");

            RuleFor(x => x.AssigneeId)
                .NotEmpty()
                .WithMessage("AssigneeId must not be empty.");
        }
    }
}
