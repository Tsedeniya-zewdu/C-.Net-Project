
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsyn(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

       public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
{
    var portfolioModel = await _context.Portfolios
        .FirstOrDefaultAsync(x =>
            x.AppUserId == appUser.Id &&
            x.Stoke.Symbol.ToLower() == symbol.ToLower()
        );

    if (portfolioModel == null)
        return null;

    _context.Portfolios.Remove(portfolioModel);
    await _context.SaveChangesAsync();

    return portfolioModel;
}

        public async Task<List<Stoke>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(p => new Stoke
                {
                    StokeId = p.Stoke.StokeId,
                    Symbol = p.Stoke.Symbol,
                    CompanyName = p.Stoke.CompanyName,
                    Purchase = p.Stoke.Purchase,
                    LastDiv = p.Stoke.LastDiv,
                    Industry = p.Stoke.Industry,
                    MarketCap = p.Stoke.MarketCap
                })
                .ToListAsync();
        }
    }
}
