using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class TIpoSecaoFilialService
    {
        private readonly TipoSecaoFilialRepository tipoSecaoFilialRepository;

        public TIpoSecaoFilialService(TipoSecaoFilialRepository tipoSecaoFilialRepository)
        {
            this.tipoSecaoFilialRepository = tipoSecaoFilialRepository;
        }

        public async Task<IEnumerable<TipoSecao>> GetAllAsync()
        {
            return await tipoSecaoFilialRepository.GetAll();
        }

        public async Task<TipoSecao?> GetByIdAsync(int id)
        {
            return await tipoSecaoFilialRepository.GetById(id);
        }

        public async Task CreateAsync(TipoSecao tipoSecao)
        {
            await tipoSecaoFilialRepository.Add(tipoSecao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await tipoSecaoFilialRepository.Delete(id);
        }

        public async Task UpdateAsync(TipoSecao tipoSecao)
        {
            await tipoSecaoFilialRepository.Update(tipoSecao);
        }
    }
}
