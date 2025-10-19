using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Service
{
    public class CidadeService
    {

        private readonly CidadeRepository cidadeRepository;

        public CidadeService(CidadeRepository cidadeRepository)
        {
            this.cidadeRepository = cidadeRepository;
        }

        public async Task<IEnumerable<Cidade>> GetgetAllCidades()
        {
            return await cidadeRepository.GetAllAsync();
        }

        public async Task<Cidade> GetgetCidadeById(int id)
        {
            var cidade = await cidadeRepository.GetByIdAsync(id);
            if (cidade == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            return cidade;
        }

        public async Task<Cidade> CreateCidade(Cidade cidade)
        {
            return await cidadeRepository.AddAsync(cidade);
        }

        public async Task<Cidade> UpdateCidade(int id, Cidade cidade)
        {

            var existingCidade = await cidadeRepository.GetByIdAsync(id);
            if (existingCidade == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            existingCidade.NomeCidade = cidade.NomeCidade;
            existingCidade.id_estado = cidade.id_estado;
            await cidadeRepository.UpdateAsync(existingCidade);
            return existingCidade;

        }

        public async Task<Cidade> DeleteCidade(int id)
        {
            var existingCidade = await cidadeRepository.GetByIdAsync(id);
            if (existingCidade == null)
            {
                throw new Exception("Cidade não encontrada");
            }
            await cidadeRepository.DeleteAsync(id);
            return existingCidade;
        }
    }
}
