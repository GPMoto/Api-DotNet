using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Repositories
{
    public class SecoesFilialRepository
    {

        private readonly AppDbContext _context;

        public SecoesFilialRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SecoesFilial>> GetAllAsync()
        {
            return await _context.SecoesFilial.ToListAsync();
        }
        public async Task<SecoesFilial?> GetByIdAsync(int id)
        {
            return await _context.SecoesFilial.FindAsync(id);
        }
        public async Task<IEnumerable<SecoesFilial>> GetByIdFilial(int idFilial)
        {
            return await _context.SecoesFilial
                .Where(s => s.id_filial == idFilial)
                .ToListAsync();
        }
        public async Task<SecoesFilial> AddAsync(SecoesFilial secoesFilial)
        {
            _context.SecoesFilial.Add(secoesFilial);
            await _context.SaveChangesAsync();
            return secoesFilial;
        }
        public async Task UpdateAsync(SecoesFilial secoesFilial)
        {
            _context.Entry(secoesFilial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var secoesFilial = await _context.SecoesFilial.FindAsync(id);
            if (secoesFilial == null)
            {
                return false;
            }
            _context.SecoesFilial.Remove(secoesFilial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
