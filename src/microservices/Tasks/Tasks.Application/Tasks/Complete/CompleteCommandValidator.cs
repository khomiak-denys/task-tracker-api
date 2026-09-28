using FluentValidation;

namespace Tasks.Application.Tasks.Complete
{
    /// <summary>Validates <see cref="CompleteCommand"/> before the handler executes.</summary>
    public class CompleteCommandValidator : AbstractValidator<CompleteCommand>
    {
        public CompleteCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.RequestedById)
                .NotEmpty().WithMessage("RequestedById is required.");
        }
    }
}
