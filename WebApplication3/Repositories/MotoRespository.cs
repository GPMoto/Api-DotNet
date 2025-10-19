using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication3.Repositories
{
    public class MotoRespository
    {
        private readonly AppDbContext _context;
        public MotoRespository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await _context.Moto.ToListAsync();
        }

        public async Task<Moto?> GetByIdAsync(int id)
        {
            return await _context.Moto.FindAsync(id);
        }

        public async Task<IEnumerable<Moto>> GetByIdentificador(string identificador)
        {
            return await _context.Moto
                .Where(m => m.IdentificadorMoto == identificador)
                .ToListAsync();
        }

        public async Task<IEnumerable<Moto>> GetByIdFilial(int id)
        {
            return await _context.Moto
                .Where(m => m.id_filial == id)
                .ToListAsync();
        }

        public async Task<Moto> AddAsync(Moto moto)
        {
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task UpdateAsync(Moto moto)
        {
            _context.Entry(moto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null)
            {
                return false;
            }
            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
