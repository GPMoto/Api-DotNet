using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class MotoService
    {

        private readonly MotoRespository motoRepository;

        public MotoService(MotoRespository repo)
        {
            this.motoRepository = repo;
        }

        public Task<IEnumerable<Moto>> GetAllAsync()
        {
            return motoRepository.GetAllAsync();


        }
        public Task<Moto?> GetByIdAsync(int id)
        {
            return motoRepository.GetByIdAsync(id);

        }

        public Task<IEnumerable<Moto>> GetByIdentificador(string identificador)
        {
            return motoRepository.GetByIdentificador(identificador);

        }

        public Task<IEnumerable<Moto>> GetByIdFilial(int id)
        {
            return motoRepository.GetByIdFilial(id);

        }

        public Task<Moto> AddAsync(Moto moto)
        {
            return motoRepository.AddAsync(moto);

        }

        public Task UpdateAsync(Moto moto)
        {
            return motoRepository.UpdateAsync(moto);

        }

        public Task<bool> DeleteAsync(int id)
        {
            return motoRepository.DeleteAsync(id);

        }
    }
}
