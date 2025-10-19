using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class SecoesFilialService
    {
        private readonly SecoesFilialRepository _secoesFilialRepository;

        public SecoesFilialService(SecoesFilialRepository secoesFilialRepository)
        {
            _secoesFilialRepository = secoesFilialRepository;
        }

        public async Task<IEnumerable<SecoesFilial>> GetAllAsync()
        {
            return await _secoesFilialRepository.GetAllAsync();
        }
        public async Task<SecoesFilial?> GetByIdAsync(int id)
        {
            return await _secoesFilialRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<SecoesFilial>> GetByIdFilial(int idFilial)
        {
            return await _secoesFilialRepository.GetByIdFilial(idFilial);
        }
        public async Task<SecoesFilial> AddAsync(SecoesFilial secoesFilial)
        {
            return await _secoesFilialRepository.AddAsync(secoesFilial);
        }
        public async Task UpdateAsync(SecoesFilial secoesFilial)
        {
            await _secoesFilialRepository.UpdateAsync(secoesFilial);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _secoesFilialRepository.DeleteAsync(id);
        }
    }
}
