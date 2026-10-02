using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tags;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.Update
{
    /// <summary>
    /// Handles <see cref="UpdateTaskCommand"/> — updates scalar fields and reconciles the tag set.
    /// </summary>
    internal sealed class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly ITagRepository _tagRepository;

        public UpdateTaskCommandHandler(
            IUnitOfWork uow,
            ITaskRepository taskRepository,
            ITagRepository tagRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _tagRepository = tagRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task is null)
            {
                return Result.Failure(new NotFoundError($"Task '{command.TaskId}' was not found."));
            }

            task.Update(command.Title, command.Description, command.Priority, command.Deadline);

            // Reconcile tags: clear existing joins and re-attach.
            task.TaskTags.Clear();
            foreach (var tagName in command.TagNames.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                var tag = await _tagRepository.GetByNameAsync(tagName, cancellationToken)
                          ?? await CreateAndRegisterTagAsync(tagName, cancellationToken);

                task.TaskTags.Add(new TaskTag(task.Id, tag.Id));
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private async Task<Tag> CreateAndRegisterTagAsync(string name, CancellationToken cancellationToken)
        {
            var tag = Tag.Create(name);
            await _tagRepository.AddAsync(tag, cancellationToken);
            return tag;
        }
    }
}
