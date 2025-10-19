using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class FilialService
    {

        private readonly FilialRepository filialRepository;

        public FilialService(FilialRepository filialRepository)
        {
            this.filialRepository = filialRepository;
        }

        public async Task<IEnumerable<Filial>> GetAllAsync()
        {
            return await filialRepository.GetAllAsync();
        }

        public async Task<Filial?> GetByIdAsync(int id)
        {
            return await filialRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Filial>> getByCNPJ(string cnpj)
        {
            return await filialRepository.getByCNPJ(cnpj);
        }

        public async Task<Filial> AddAsync(Filial filial)
        {
            return await filialRepository.AddAsync(filial);
        }

        public async Task UpdateAsync(Filial filial)
        {
            await filialRepository.UpdateAsync(filial);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await filialRepository.DeleteAsync(id);
        }

    }
}
