using DomainFramework.Results;
using Messaging.Abstractions;

namespace Tasks.Application.Tasks.Assign
{
    public record AssignCommand(
        Guid TaskId,
        Guid AssigneeId,
        Guid RequestedById) : ICommand<Result>;
}
