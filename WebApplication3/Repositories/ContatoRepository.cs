using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Repositories
{
    public class ContatoRepository
    {
        private readonly AppDbContext _context;

        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync()
        {
            return await _context.Contato.ToListAsync();
        }

        public async Task<Contato?> GetByIdAsync(int id)
        {
            return await _context.Contato.FindAsync(id);
        }

        public async Task<IEnumerable<Contato>> GetByNameDonoAsync(string nomeDono)
        {
            return await _context.Contato
                .Where(c => c.nmDono.Contains(nomeDono))
                .ToListAsync();
        }

        public async Task<Contato> AddAsync(Contato contato)
        {
            _context.Contato.Add(contato);
            await _context.SaveChangesAsync();
            return contato;
        }

        public async Task UpdateAsync(Contato contato)
        {
            _context.Entry(contato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return false;
            }
            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
