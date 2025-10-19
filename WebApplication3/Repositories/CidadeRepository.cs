using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class CidadeRepository
    {

        private readonly AppDbContext _context;
        public CidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cidade>> GetAllAsync()
        {
            return await _context.Cidade.ToListAsync();
        }

        public async Task<Cidade?> GetByIdAsync(int id)
        {
            return await _context.Cidade.FindAsync(id);
        }

        public async Task<Cidade> AddAsync(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();
            return cidade;
        }
        public async Task UpdateAsync(Cidade cidade)
        {
            _context.Entry(cidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return false;
            }

            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
