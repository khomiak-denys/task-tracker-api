using DomainFramework.Results;
using Users.Application.Roles;

namespace Users.Application.Interfaces
{
    public interface IRoleService
    {
        Task<Result<IReadOnlyList<string>>> GetAllAsync(CancellationToken ct);
        Task<Result> AssignAsync(Guid userId, AppRole role, CancellationToken ct);
        Task<Result> RemoveAsync(Guid userId, AppRole role, CancellationToken ct);
        Task<Result<IReadOnlyList<string>>> GetForUserAsync(Guid userId, CancellationToken ct);
    }
}
