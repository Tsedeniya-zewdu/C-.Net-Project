using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        public Task<List<Stoke>> GetUserStokesAsync(string userId, CancellationToken ct = default)
            => context.Portfolios
                .AsNoTracking()
                .Where(p => p.AppUserId == userId)
                .Select(p => p.Stoke)
                .OrderBy(s => s.Symbol)
                .ToListAsync(ct);

        public Task<Portfolio?> GetAsync(string userId, int stokeId, CancellationToken ct = default)
            => context.Portfolios
                .FirstOrDefaultAsync(p => p.AppUserId == userId && p.StokeId == stokeId, ct);

        public async Task AddAsync(Portfolio portfolio, CancellationToken ct = default)
            => await context.Portfolios.AddAsync(portfolio, ct);

        public void Remove(Portfolio portfolio) => context.Portfolios.Remove(portfolio);
    }
}
