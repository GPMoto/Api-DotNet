using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class TipoSecaoFilialRepository
    {
        private readonly AppDbContext _context;
        public TipoSecaoFilialRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoSecao>> GetAll()
        {
            return _context.TipoSecao.ToList();
        }

        public async Task<TipoSecao?> GetById(int id)
        {
            return await _context.TipoSecao.FindAsync(id);
        }

        public async Task Add(TipoSecao tipoSecao)
        {
            _context.TipoSecao.Add(tipoSecao);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TipoSecao tipoSecao)
        {
            _context.Entry(tipoSecao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var tipoSecao = await _context.TipoSecao.FindAsync(id);
            if (tipoSecao == null)
            {
                return false;
            }
            _context.TipoSecao.Remove(tipoSecao);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
