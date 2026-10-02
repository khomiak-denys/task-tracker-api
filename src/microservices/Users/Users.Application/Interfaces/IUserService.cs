using DomainFramework;
using DomainFramework.Results;
using Users.Application.DTOs;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserProfileResult>> GetByIdAsync(Guid userId, CancellationToken ct);

        /// <summary>
        /// Retrieves contact information for a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The user's contact information if found; otherwise, a failure result.</returns>
        Task<Result<UserContactInfoResult>> GetContactInfoByIdAsync(Guid userId, CancellationToken ct);

        Task<Result<PaginationResult<UserResult>>> GetAllAsync(int page, int pageSize, CancellationToken ct);
        Task<Result> UpdateProfileAsync(Guid userId, string? fullName, string? userName, CancellationToken ct);
        Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken ct);
        Task<Result> DeleteAsync(Guid userId, CancellationToken ct);
    }
}
