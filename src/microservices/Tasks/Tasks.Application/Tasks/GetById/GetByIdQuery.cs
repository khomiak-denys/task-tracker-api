using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Tasks.DTOs;

namespace Tasks.Application.Tasks.GetById
{
    public record GetByIdQuery(
        Guid TaskId,
        Guid RequestingUserId,
        bool IsAdmin) : IQuery<Result<TaskDetailsResult>>;
}
