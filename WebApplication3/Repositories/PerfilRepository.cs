using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class PerfilRepository
    {

        private readonly AppDbContext _context;
        public PerfilRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            return await _context.Perfil.ToListAsync();
        }
        public async Task<Perfil?> GetByIdAsync(int id)
        {
            return await _context.Perfil.FindAsync(id);
        }
        public async Task<Perfil> AddAsync(Perfil perfil)
        {
            _context.Perfil.Add(perfil);
            await  _context.SaveChangesAsync();
            return perfil;
        }
        public async Task UpdateAsync(Perfil perfil, int id)
        {
            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return false;
            }
            _context.Perfil.Remove(perfil);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
