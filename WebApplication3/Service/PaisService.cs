using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class PaisService
    {

        private readonly PaisRepository _paisRepository;

        public PaisService(PaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }

        public async Task<IEnumerable<Pais>> GetAllAsync()
        {
            return await _paisRepository.GetAllAsync();
        }
        public async Task<Pais?> GetByIdAsync(int id)
        {
            return await _paisRepository.GetByIdAsync(id);
        }
        public async Task<Pais> AddAsync(Pais pais)
        {
            return await _paisRepository.AddAsync(pais);
        }
        public async Task UpdateAsync(Pais pais)
        {
            await _paisRepository.UpdateAsync(pais);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _paisRepository.DeleteAsync(id);
        }
    }
}
