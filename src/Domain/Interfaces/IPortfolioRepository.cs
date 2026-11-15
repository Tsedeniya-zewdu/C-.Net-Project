using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stoke>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsyn(Portfolio portfolio);
Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
    }
}