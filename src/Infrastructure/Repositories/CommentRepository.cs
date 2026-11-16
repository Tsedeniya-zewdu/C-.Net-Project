using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync(CancellationToken ct = default)
            => context.Comment
                .AsNoTracking()
                .Include(c => c.AppUser)
                .OrderBy(c => c.Id)
                .ToListAsync(ct);

        // Tracked (no AsNoTracking) because update and delete handlers change the result.
        public Task<Comment?> GetByIdAsync(int id, CancellationToken ct = default)
            => context.Comment
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task AddAsync(Comment comment, CancellationToken ct = default)
            => await context.Comment.AddAsync(comment, ct);

        public void Remove(Comment comment) => context.Comment.Remove(comment);
    }
}
