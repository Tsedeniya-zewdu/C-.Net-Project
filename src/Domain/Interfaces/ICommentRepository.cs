using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CancellationToken ct = default);
        Task<Comment?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Comment comment, CancellationToken ct = default);
        void Remove(Comment comment);
    }
}
