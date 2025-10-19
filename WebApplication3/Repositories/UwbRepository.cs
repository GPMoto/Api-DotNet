using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class UwbRepository
    {
        private readonly AppDbContext _context;
        public UwbRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Uwb>> GetAll()
        {
            return await _context.Uwb.ToListAsync();
        }
        public async Task<Uwb?> GetById(int id)
        {
            return await _context.Uwb.FindAsync(id);
        }
        public async Task Add(Uwb uwb)
        {
            _context.Uwb.Add(uwb);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Uwb uwb)
        {
            _context.Entry(uwb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var uwb = await _context.Uwb.FindAsync(id);
            if (uwb == null)
            {
                return false;
            }
            _context.Uwb.Remove(uwb);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
