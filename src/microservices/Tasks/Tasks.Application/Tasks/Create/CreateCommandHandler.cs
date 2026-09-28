using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tags;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.Create
{
    /// <summary>
    /// Handles <see cref="CreateCommand"/> — creates a new <see cref="TaskItem"/> and attaches tags.
    /// </summary>
    internal sealed class CreateCommandHandler : ICommandHandler<CreateCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly ITagRepository _tagRepository;

        public CreateCommandHandler(
            IUnitOfWork uow,
            ITaskRepository taskRepository,
            ITagRepository tagRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _tagRepository = tagRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            var task = TaskItem.Create(
                command.Title,
                command.Description,
                command.Priority,
                command.Deadline,
                command.AssigneeId,
                command.CreatedById);

            // Resolve or create tags and attach them.
            foreach (var tagName in command.TagNames.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                var tag = await _tagRepository.GetByNameAsync(tagName, cancellationToken)
                          ?? await CreateAndRegisterTagAsync(tagName, cancellationToken);

                task.TaskTags.Add(new TaskTag(task.Id, tag.Id));
            }

            await _taskRepository.AddAsync(task, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(task.Id);
        }

        private async Task<Tag> CreateAndRegisterTagAsync(string name, CancellationToken cancellationToken)
        {
            var tag = Tag.Create(name);
            await _tagRepository.AddAsync(tag, cancellationToken);
            return tag;
        }
    }
}
