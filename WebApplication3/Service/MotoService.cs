using WebApplication3.Models;

namespace WebApplication3.Service
{
    public class MotoService
    {

        private readonly MotoService motoService;

        public MotoService(MotoService motoService)
        {
            this.motoService = motoService;
        }

        public Task<IEnumerable<Moto>> GetAllAsync()
        {
            return motoService.GetAllAsync();
        }
        public Task<Moto?> GetByIdAsync(int id)
        {
            return motoService.GetByIdAsync(id);
        }

        public Task<IEnumerable<Moto>> GetByIdentificador(string identificador)
        {
            return motoService.GetByIdentificador(identificador);
        }

        public Task<IEnumerable<Moto>> GetByIdFilial(int id)
        {
            return motoService.GetByIdFilial(id);
        }

        public Task<Moto> AddAsync(Moto moto)
        {
            return motoService.AddAsync(moto);
        }

        public Task UpdateAsync(Moto moto)
        {
            return motoService.UpdateAsync(moto);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return motoService.DeleteAsync(id);
        }
    }
}
