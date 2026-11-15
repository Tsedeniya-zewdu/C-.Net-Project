
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stoke;
using api.Helpers;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StokeRepository : IStokeRepository
    {
        private readonly ApplicationDBContext _context;

        public StokeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<Stoke> CreateAsync(Stoke stokeModel)
        {
            await _context.Stoke.AddAsync(stokeModel);
            await _context.SaveChangesAsync();

            return stokeModel;
        }

        // GET ALL
        public async Task<List<Stoke>> GetAllAsync(QueryObjects query)
        {
            var stokes = _context.Stoke
                .Include(c => c.Comments).ThenInclude(a=>a.AppUser)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stokes = stokes.Where(s =>
                    s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stokes = stokes.Where(s =>
                    s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stokes = query.IsAscending
                        ? stokes.OrderBy(s => s.Symbol)
                        : stokes.OrderByDescending(s => s.Symbol);
                }
                else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stokes = query.IsAscending
                        ? stokes.OrderBy(s => s.CompanyName)
                        : stokes.OrderByDescending(s => s.CompanyName);
                }
            }
          var skipNumber =(query.PageNumber -1) * query.PageSize; 
            return await stokes.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        // GET BY ID
        public async Task<Stoke?> GetByIdAsync(int id)
        {
            return await _context.Stoke.Include(c => c.Comments).FirstOrDefaultAsync(i => i.StokeId == id);
        }

        // UPDATE
        public async Task<Stoke?> UpdateAsync(
            int id,
            UpdateStokeRequestDto stokeDto)
        {
            var existingStoke = await _context.Stoke
                .FirstOrDefaultAsync(x => x.StokeId == id);

            if (existingStoke == null)
            {
                return null;
            }

            existingStoke.Symbol = stokeDto.Symbol;
            existingStoke.CompanyName = stokeDto.CompanyNAme;
            existingStoke.Purchase = stokeDto.Purchase;
            existingStoke.LastDiv = stokeDto.LastDiv;
            existingStoke.Industry = stokeDto.Industry;

            await _context.SaveChangesAsync();

            return existingStoke;
        }

        // DELETE
        public async Task<Stoke?> DeleteAsync(int id)
        {
            var stoke = await _context.Stoke
                .FirstOrDefaultAsync(x => x.StokeId == id);

            if (stoke == null)
            {
                return null;
            }

            _context.Stoke.Remove(stoke);

            await _context.SaveChangesAsync();

            return stoke;
        }

        public Task<bool> StokeExists(int id)
        {
            return _context.Stoke.AnyAsync(i => i.StokeId == id);
        }

        public async Task<Stoke?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stoke.FirstOrDefaultAsync(s=> s.Symbol==symbol);
        }
    }
}
