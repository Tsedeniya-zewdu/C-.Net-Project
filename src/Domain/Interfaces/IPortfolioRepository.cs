using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stoke>> GetUserStokesAsync(string userId, CancellationToken ct = default);
        Task<Portfolio?> GetAsync(string userId, int stokeId, CancellationToken ct = default);
        Task AddAsync(Portfolio portfolio, CancellationToken ct = default);
        void Remove(Portfolio portfolio);
    }
}
