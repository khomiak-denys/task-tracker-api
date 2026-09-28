using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Tasks.DTOs;

namespace Tasks.Application.Tasks.GetAll
{
    public record GetAllQuery(int Page = 1, int PageSize = 10) : IQuery<Result<PaginationResult<TaskResult>>>;
}
