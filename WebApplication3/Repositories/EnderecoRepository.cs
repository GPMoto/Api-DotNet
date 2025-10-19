using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class EnderecoRepository
    {

        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endereco>> GetAllAsync()
        {
            return await _context.Endereco.ToListAsync();
        }

        public async Task<Endereco?> GetByIdAsync(int id)
        {
            return await _context.Endereco.FindAsync(id);

        }

        public async Task<IEnumerable<Endereco>> GetByCep(string cep)
        {
            return await _context.Endereco
                .Where(e => e.Cep == cep)
                .ToListAsync();
        }

        public async Task<Endereco> AddAsync(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task UpdateAsync(Endereco endereco)
        {
            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return false;
            }
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
