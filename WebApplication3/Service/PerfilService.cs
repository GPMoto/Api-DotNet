using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class PerfilService
    {
        private readonly PerfilRepository _perfilRepository;

        public PerfilService(PerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<IEnumerable<Perfil>> GetAllPerfisAsync()
        {
            return await _perfilRepository.GetAllAsync();
        }
        public async Task<Perfil?> GetPerfilByIdAsync(int id)
        {
            return await _perfilRepository.GetByIdAsync(id);
        }
        public async Task<Perfil> CreatePerfilAsync(Perfil perfil)
        {
            return await _perfilRepository.AddAsync(perfil);
        }
        public async Task UpdatePerfilAsync(Perfil perfil)
        {
            await _perfilRepository.UpdateAsync(perfil);
        }
        public async Task<bool> DeletePerfilAsync(int id)
        {
            return await _perfilRepository.DeleteAsync(id);
        }
    }
}
