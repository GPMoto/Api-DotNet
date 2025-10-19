using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class TipoMotoRepository
    {

        private readonly AppDbContext _context;
        public TipoMotoRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TipoMoto>> GetAllAsync()
        {
            return await _context.TipoMoto.ToListAsync();
        }

        public async Task<TipoMoto?> GetByIdAsync(int id)
        {
            return await _context.TipoMoto.FindAsync(id);
        }

        public async Task<TipoMoto> AddAsync(TipoMoto tipoMoto)
        {
            _context.TipoMoto.Add(tipoMoto);
            await _context.SaveChangesAsync();
            return tipoMoto;
        }

        public async Task UpdateAsync(TipoMoto tipoMoto)
        {
            _context.Entry(tipoMoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipoMoto = await _context.TipoMoto.FindAsync(id);
            if (tipoMoto == null)
            {
                return false;
            }
            _context.TipoMoto.Remove(tipoMoto);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
