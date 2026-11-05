using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Dtos.Stoke;
using api.Models;

namespace api.interfaces
{
    public interface IStokeRepository
    {
        Task<List<Stoke>> GetAllAsync();
        Task<Stoke?> GetByIdAsync(int id);
        Task<Stoke> CreateAsync(Stoke stokeModel);
        Task<Stoke?> UpdateAsync(int id,UpdateStokeRequestDto stokezdto);
        Task<Stoke?> DeleteAsync(int id);
    }
}