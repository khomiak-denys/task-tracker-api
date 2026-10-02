using FluentValidation;

namespace Tasks.Application.Tasks.GetById
{
    /// <summary>
    /// Validates <see cref="GetTaskByIdQuery"/> before execution.
    /// </summary>
    public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
    {
        /// <summary>Initializes a new instance of <see cref="GetTaskByIdQueryValidator"/>.</summary>
        public GetTaskByIdQueryValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty()
                .WithMessage("TaskId must not be empty.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty()
                .WithMessage("RequestingUserId must not be empty.");
        }
    }
}
