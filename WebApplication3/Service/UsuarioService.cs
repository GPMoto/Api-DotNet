using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class UsuarioService
    {

        private readonly UsuarioRepository usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await usuarioRepository.GetAllAsync();
        }
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await usuarioRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Usuario>> getByIdFilial(int idFilial)
        {
            return await usuarioRepository.GetByIdFilial(idFilial);
        }
        public async Task<Usuario?> getByEmail(string email)
        {   
            var usuario = await usuarioRepository.GetByEmailAsync(email);
            if (usuario != null)
            {
                return usuario;
            }
            throw new Exception("Usuário não encontrado");
        }
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            return await usuarioRepository.AddAsync(usuario);
        }
        public async Task UpdateAsync(Usuario usuario)
        {
            await usuarioRepository.UpdateAsync(usuario);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await usuarioRepository.DeleteAsync(id);
        }

    }
}
