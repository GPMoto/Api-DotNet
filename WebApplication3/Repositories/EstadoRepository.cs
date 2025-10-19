using WebApplication3.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;


namespace WebApplication3.Repositories
{
    public class EstadoRepository
    {
        private readonly AppDbContext _context;

        public EstadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estado>> GetAllAsync()
        {
            return await _context.Estado.ToListAsync();
        }

        public async Task<Estado?> GetByIdAsync(int id)
        {
            return await _context.Estado.FindAsync(id);
        }

        public async Task<Estado> AddAsync(Estado estado)
        {
            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task UpdateAsync(Estado estado, int id)
        {
            _context.Entry(estado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return false;
            }
            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
