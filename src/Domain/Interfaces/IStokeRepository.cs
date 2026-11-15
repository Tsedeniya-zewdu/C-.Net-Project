using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Filters;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStokeRepository
    {
        Task<List<Stoke>> GetAllAsync(QueryObjects query);
        Task<Stoke?> GetByIdAsync(int id);
        Task<Stoke?> GetBySymbolAsync(string symbol);
        Task<Stoke> CreateAsync(Stoke stokeModel);
        Task<Stoke> UpdateAsync(Stoke stoke);
        Task<Stoke?> DeleteAsync(int id);
        Task<bool>StokeExists(int id);
    }
}