using Microsoft.EntityFrameworkCore;
using Workspaces.Domain.Tags;

namespace Workspaces.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// EF Core implementation of <see cref="ITagRepository"/>.
    /// </summary>
    internal sealed class TagRepository : ITagRepository
    {
        private readonly WorkspacesDbContext _context;

        public TagRepository(WorkspacesDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t => EF.Functions.ILike(t.Name, name), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddAsync(Tag tag, CancellationToken cancellationToken)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
        }
    }
}
