using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaces
{
    // Repositories only read and stage changes.
    // Saving is done once per request by IUnitOfWork (in the Application layer).
    public interface IStokeRepository
    {
        Task<List<Stoke>> GetAllAsync(QueryObjects query, CancellationToken ct = default);
        Task<Stoke?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Stoke?> GetBySymbolAsync(string symbol, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
        Task<bool> SymbolExistsAsync(string symbol, int? excludeId = null, CancellationToken ct = default);
        Task AddAsync(Stoke stoke, CancellationToken ct = default);
        void Remove(Stoke stoke);
    }
}
