using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class EnderecoService
    {

        private readonly EnderecoRepository enderecoRepository;
        public EnderecoService(EnderecoRepository enderecoRepository)
        {
            this.enderecoRepository = enderecoRepository;
        }

        public async Task<IEnumerable<Endereco>> GetAllAsync()
        {
            return await enderecoRepository.GetAllAsync();
        }

        public async Task<Endereco?> GetByIdAsync(int id)
        {
            return await enderecoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Endereco>> GetByCep(string cep)
        {
            return await enderecoRepository.GetByCep(cep);
        }

        public async Task<Endereco> AddAsync(Endereco endereco)
        {
            return await enderecoRepository.AddAsync(endereco);
        }

        public async Task UpdateAsync(Endereco endereco)
        {
            await enderecoRepository.UpdateAsync(endereco);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await enderecoRepository.DeleteAsync(id);
        }


    }
}
