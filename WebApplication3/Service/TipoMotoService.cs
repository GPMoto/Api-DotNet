using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class TipoMotoService
    {

        private readonly TipoMotoRepository tipoMotoRepository;

        public TipoMotoService(TipoMotoRepository tipoMotoRepository)
        {
            this.tipoMotoRepository = tipoMotoRepository;
        }

        public async Task<IEnumerable<TipoMoto>> GetAllTipoMotoAsync()
        {
            return await tipoMotoRepository.GetAllAsync();
        }

        public async Task<TipoMoto?> GetTipoMotoByIdAsync(int id)
        {
            return await tipoMotoRepository.GetByIdAsync(id);
        }

        public async Task<TipoMoto> CreateTipoMotoAsync(TipoMoto tipoMoto)
        {
            return await tipoMotoRepository.AddAsync(tipoMoto);
        }
        public async Task<bool> DeleteTipoMotoAsync(int id)
        {
            return await tipoMotoRepository.DeleteAsync(id);
        }
        public async Task UpdateTipoMotoAsync(TipoMoto tipoMoto)
        {
            await tipoMotoRepository.UpdateAsync(tipoMoto);
        }


    }
}
