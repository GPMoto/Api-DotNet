using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class TelefoneRepository
    {

        private readonly AppDbContext _context;
        public TelefoneRepository(AppDbContext context)
        {
            _context = context;


        }

        public async Task<IEnumerable<Telefone>> GetAll()
        {
            return await _context.Telefone.ToListAsync();
        }

        public async Task<Telefone?> GetById(int id)
        {
            return await _context.Telefone.FindAsync(id);
        }

        public async Task<Telefone> Add(Telefone telefone)
        {
            _context.Telefone.Add(telefone);
            await _context.SaveChangesAsync();
            return telefone;
        }

        public async Task Update(Telefone telefone)
        {
            _context.Entry(telefone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var telefone = await _context.Telefone.FindAsync(id);
            if (telefone == null)
            {
                return false;
            }
            _context.Telefone.Remove(telefone);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
