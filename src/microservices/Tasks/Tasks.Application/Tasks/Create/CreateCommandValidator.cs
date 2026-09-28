using FluentValidation;

namespace Tasks.Application.Tasks.Create
{
    /// <summary>Validates <see cref="CreateCommand"/> before the handler executes.</summary>
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.CreatedById)
                .NotEmpty().WithMessage("CreatedById is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.")
                .When(x => x.Description is not null);

            RuleFor(x => x.Deadline)
                .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future.")
                .When(x => x.Deadline.HasValue);

            RuleForEach(x => x.TagNames)
                .NotEmpty().WithMessage("Tag name cannot be empty.")
                .MaximumLength(50).WithMessage("Tag name cannot exceed 50 characters.");
        }
    }
}
