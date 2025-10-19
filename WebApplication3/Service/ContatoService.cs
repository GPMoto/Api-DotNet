using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class ContatoService
    {

        private readonly ContatoRepository contatoRepository;

        public ContatoService(ContatoRepository repo)
        {
            contatoRepository = repo;
        }

        
        public async Task<IEnumerable<Contato>> GetAllContatos()
        {
            return await contatoRepository.GetAllAsync();
        }
        public async Task<Contato?> GetById(int id)
        {
            return await contatoRepository.GetByIdAsync(id);
        }


        public async Task<IEnumerable<Contato>> GetByNameDono(string nomeDono)
        {
            return await contatoRepository.GetByNameDonoAsync(nomeDono);
        }

        public async Task<Contato> CreateContato(Contato contato)
        {
            return await contatoRepository.AddAsync(contato);
        }

        public async Task<Contato?> UpdateContato(int id, Contato contato)
        {
            var existingContato = await contatoRepository.GetByIdAsync(id);
            await contatoRepository.UpdateAsync(existingContato);
            return existingContato;

        }

        public async Task<bool> DeleteContato(int id)
        {
            return await contatoRepository.DeleteAsync(id);
        }

    }
}
