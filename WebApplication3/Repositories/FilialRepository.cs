using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication3.Repositories
{
    public class FilialRepository
    {

        private readonly AppDbContext _context;

        public FilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filial>> GetAllAsync()
        {
            return await _context.Filial.ToListAsync();
        }

        public async Task<Filial?> GetByIdAsync(int id)
        {
            return await _context.Filial.FindAsync(id);

        }

        public async Task<IEnumerable<Filial>> getByCNPJ(string cnpj)
        {
            return await _context.Filial
                .Where(f => f.Cnpj == cnpj)
                .ToListAsync();
        }

        public async Task<Filial> AddAsync(Filial filial)
        {
            _context.Filial.Add(filial);
            await _context.SaveChangesAsync();
            return filial;
        }

        public async Task UpdateAsync(Filial filial)
        {
            _context.Entry(filial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            if (filial == null)
            {
                return false;
            }
            _context.Filial.Remove(filial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
