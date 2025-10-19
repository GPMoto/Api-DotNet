using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class TelefoneService
    {

        private readonly TelefoneRepository telefoneRepository;
        public TelefoneService(TelefoneRepository telefoneRepository)
        {
            this.telefoneRepository = telefoneRepository;
        }

        public async Task<IEnumerable<Telefone>> GetAllAsync()
        {
            return await telefoneRepository.GetAll();
        }
        public async Task<Telefone?> GetByIdAsync(int id)
        {
            return await telefoneRepository.GetById(id);
        }
        public async Task<Telefone> CreateAsync(Telefone telefone)
        {
            await telefoneRepository.Add(telefone);
            return telefone;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await telefoneRepository.Delete(id);
        }
        public async Task UpdateAsync(Telefone telefone)
        {
             await telefoneRepository.Update(telefone);
        }

    }
}
