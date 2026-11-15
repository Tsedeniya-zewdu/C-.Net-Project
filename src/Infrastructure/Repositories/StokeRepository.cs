using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    // EF Core implementation of the Domain interface.
    // Note: no SaveChangesAsync here. Handlers commit through IUnitOfWork.
    public class StokeRepository(ApplicationDBContext context) : IStokeRepository
    {
        public async Task<List<Stoke>> GetAllAsync(QueryObjects query, CancellationToken ct = default)
        {
            // AsNoTracking: read-only query, so EF does not need to watch these entities for changes.
            var stokes = context.Stoke
                .AsNoTracking()
                .Include(s => s.Comments).ThenInclude(c => c.AppUser)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stokes = stokes.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stokes = stokes.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            stokes = query.SortBy?.ToLowerInvariant() switch
            {
                "symbol" => query.IsAscending
                    ? stokes.OrderBy(s => s.Symbol)
                    : stokes.OrderByDescending(s => s.Symbol),
                "companyname" => query.IsAscending
                    ? stokes.OrderBy(s => s.CompanyName)
                    : stokes.OrderByDescending(s => s.CompanyName),
                // Paging needs a fixed order, otherwise pages can repeat or skip rows.
                _ => stokes.OrderBy(s => s.StokeId)
            };

            var skip = (query.PageNumber - 1) * query.PageSize;
            return await stokes.Skip(skip).Take(query.PageSize).ToListAsync(ct);
        }

        public Task<Stoke?> GetByIdAsync(int id, CancellationToken ct = default)
            => context.Stoke
                .Include(s => s.Comments).ThenInclude(c => c.AppUser)
                .FirstOrDefaultAsync(s => s.StokeId == id, ct);

        // Case-insensitive, so "aapl" finds "AAPL".
        public Task<Stoke?> GetBySymbolAsync(string symbol, CancellationToken ct = default)
            => context.Stoke
                .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower(), ct);

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
            => context.Stoke.AnyAsync(s => s.StokeId == id, ct);

        public Task<bool> SymbolExistsAsync(string symbol, int? excludeId = null, CancellationToken ct = default)
            => context.Stoke.AnyAsync(
                s => s.Symbol.ToLower() == symbol.ToLower()
                     && (excludeId == null || s.StokeId != excludeId),
                ct);

        public async Task AddAsync(Stoke stoke, CancellationToken ct = default)
            => await context.Stoke.AddAsync(stoke, ct);

        public void Remove(Stoke stoke) => context.Stoke.Remove(stoke);
    }
}
