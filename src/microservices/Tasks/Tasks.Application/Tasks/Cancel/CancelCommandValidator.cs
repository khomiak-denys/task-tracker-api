using FluentValidation;

namespace Tasks.Application.Tasks.Cancel
{
    /// <summary>Validates <see cref="CancelCommand"/> before the handler executes.</summary>
    public class CancelCommandValidator : AbstractValidator<CancelCommand>
    {
        public CancelCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.RequestedById)
                .NotEmpty().WithMessage("RequestedById is required.");
        }
    }
}
