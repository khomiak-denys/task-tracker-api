using DomainFramework;
using DomainFramework.Results;
using Users.Application.DTOs;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserProfileResult>> GetByIdAsync(Guid userId, CancellationToken ct);
        Task<Result<PaginationResult<UserResult>>> GetAllAsync(int page, int pageSize, CancellationToken ct);
        Task<Result> UpdateProfileAsync(Guid userId, string? fullName, string? userName, CancellationToken ct);
        Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken ct);
        Task<Result> DeleteAsync(Guid userId, CancellationToken ct);
    }
}
