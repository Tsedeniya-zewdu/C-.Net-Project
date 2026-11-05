
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stoke;
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
        public async Task<List<Stoke>> GetAllAsync()
        {
            return await _context.Stoke.ToListAsync();
        }

        // GET BY ID
        public async Task<Stoke?> GetByIdAsync(int id)
        {
            return await _context.Stoke.FindAsync(id);
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
            existingStoke.CompanyNAme = stokeDto.CompanyNAme;
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
    }
}
