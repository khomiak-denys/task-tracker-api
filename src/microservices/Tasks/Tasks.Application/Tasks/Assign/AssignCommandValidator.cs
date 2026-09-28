using FluentValidation;

namespace Tasks.Application.Tasks.Assign
{
    public class AssignCommandValidator : AbstractValidator<AssignCommand>
    {
        public AssignCommandValidator()
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
