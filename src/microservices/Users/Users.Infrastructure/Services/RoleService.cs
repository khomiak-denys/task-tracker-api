using DomainFramework.Errors;
using DomainFramework.Results;
using Microsoft.AspNetCore.Identity;
using Users.Application.Interfaces;
using Users.Application.Roles;
using Users.Infrastructure.Identity;

namespace Users.Infrastructure.Services
{
    internal sealed class RoleService : IRoleService
    {
        private static readonly IReadOnlyList<string> AllRoleNames = Enum.GetNames<AppRole>();
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<Result<IReadOnlyList<string>>> GetAllAsync(CancellationToken ct)
        {
            return Task.FromResult(Result<IReadOnlyList<string>>.Success(AllRoleNames));
        }

        public async Task<Result> AssignAsync(Guid userId, AppRole role, CancellationToken ct)
        {
            if (!Enum.IsDefined(role))
            {
                return Result.Failure(new InvalidArgumentError("Invalid role specified."));
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result.Failure(new NotFoundError("User not found"));
            }

            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            if (!result.Succeeded)
            {
                return Result.Failure(new InvalidArgumentError(string.Join(", ", result.Errors.Select(e => e.Description))));
            }

            return Result.Success();
        }

        public async Task<Result> RemoveAsync(Guid userId, AppRole role, CancellationToken ct)
        {
            if (!Enum.IsDefined(role))
            {
                return Result.Failure(new InvalidArgumentError("Invalid role specified."));
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result.Failure(new NotFoundError("User not found"));
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
            if (!result.Succeeded)
            {
                return Result.Failure(new InvalidArgumentError(string.Join(", ", result.Errors.Select(e => e.Description))));
            }

            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<string>>> GetForUserAsync(Guid userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<IReadOnlyList<string>>.Failure(new NotFoundError("User not found"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Success(roles.ToList());
        }
    }
}
