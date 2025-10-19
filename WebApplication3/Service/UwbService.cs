using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class UwbService
    {
        private readonly UwbRepository uwbRepository;

        public UwbService(UwbRepository uwbRepository)
        {
            this.uwbRepository = uwbRepository;
        }

        public async Task<IEnumerable<Uwb>> GetAllTagsAsync()
        {
            return await uwbRepository.GetAll();
        }
        public async Task<Uwb?> GetTagByIdAsync(int id)
        {
            return await uwbRepository.GetById(id);
        }

        public async Task CreateTagAsync(Uwb uwb)
        {
            await uwbRepository.Add(uwb);
        }
        public async Task UpdateTagAsync(Uwb uwb)
        {
            await uwbRepository.Update(uwb);
        }
        public async Task<bool> DeleteTagAsync(int id)
        {
            return await uwbRepository.Delete(id);
        }
    }
}
