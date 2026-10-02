using FluentValidation;

namespace Tasks.Application.Tasks.Complete
{
    /// <summary>Validates <see cref="CompleteTaskCommand"/> before the handler executes.</summary>
    public class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
    {
        /// <summary>Initializes a new instance of <see cref="CompleteTaskCommandValidator"/>.</summary>
        public CompleteTaskCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.RequestedById)
                .NotEmpty().WithMessage("RequestedById is required.");
        }
    }
}
