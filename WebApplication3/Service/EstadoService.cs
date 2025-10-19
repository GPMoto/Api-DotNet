using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class EstadoService
    {

        private readonly EstadoRepository estadoRepository;

        public EstadoService(EstadoRepository estadoRepository)
        {
            this.estadoRepository = estadoRepository;
        }

        public async Task<IEnumerable<Estado>> GetAllEstadosAsync()
        {
            return await estadoRepository.GetAllAsync();
        }

        public async Task<Estado?> GetEstadoByIdAsync(int id)
        {
            return await estadoRepository.GetByIdAsync(id);
        }

        public async Task<Estado> AddEstadoAsync(Estado estado)
        {
            return await estadoRepository.AddAsync(estado);
        }

        public async Task UpdateEstadoAsync(Estado estado)
        {
            await estadoRepository.UpdateAsync(estado);
        }

        public async Task<bool> DeleteEstadoAsync(int id)
        {
            return await estadoRepository.DeleteAsync(id);
        }


    }
}
