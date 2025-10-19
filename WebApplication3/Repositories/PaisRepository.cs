using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class PaisRepository
    {

        private readonly AppDbContext _context;
        public PaisRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pais>> GetAllAsync()
        {
            return await _context.Pais.ToListAsync();
        }
        public async Task<Pais?> GetByIdAsync(int id)
        {
            return await _context.Pais.FindAsync(id);
        }
        public async Task<Pais> AddAsync(Pais pais)
        {
            _context.Pais.Add(pais);
            await  _context.SaveChangesAsync();
            return pais;
        }
        public async Task UpdateAsync(Pais pais)
        {
            _context.Entry(pais).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return false;
            }
            _context.Pais.Remove(pais);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
