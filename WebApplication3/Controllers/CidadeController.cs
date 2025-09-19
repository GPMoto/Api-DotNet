using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de cidades no sistema GPMoto
    /// </summary>
    /// <remarks>
    /// Este controller permite operações CRUD completas para cidades, 
    /// incluindo listagem, consulta por ID, criação, atualização e remoção.
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Tags("Cidades")]
    public class CidadeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CidadeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as cidades cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /cidades
        ///
        /// </remarks>
        /// <returns>Lista de cidades.</returns>
        /// <response code="200">Retorna a lista de cidades.</response>
        [HttpGet("/cidades")]
        [ProducesResponseType(typeof(IEnumerable<Cidade>), 200)]
        public async Task<ActionResult<IEnumerable<Cidade>>> Get()
        {
            return await _context.Cidade.ToListAsync();
        }


        /// <summary>
        /// Retorna uma cidade pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /cidades/1
        ///
        /// </remarks>
        /// <param name="id">ID da cidade.</param>
        /// <returns>Dados da cidade.</returns>
        /// <response code="200">Retorna a cidade encontrada.</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpGet("/cidades/{id}")]
        [ProducesResponseType(typeof(Cidade), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cidade>> GetById(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound(new { message = "Cidade não encontrada" });
            }
            return Ok(cidade);
        }


         /// <summary>
        /// Cria uma nova cidade.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /cidades
        ///     {
        ///         "id_cidade": 1,
        ///         "NomeCidade": "São Paulo",
        ///         "id_estado": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="cidade">Dados da cidade.</param>
        /// <returns>Cidade criada.</returns>
        /// <response code="201">Cidade criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("/cidades")]
        [ProducesResponseType(typeof(Cidade), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cidade>> Post([FromBody] Cidade cidade)
        {
            if (cidade == null)
            {
                return BadRequest(new { message = "Cidade não pode ser nula" });
            }
            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cidade.id_cidade }, cidade);
        }

        /// <summary>
        /// Atualiza os dados de uma cidade existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /cidades/1
        ///     {
        ///         "id_cidade": 1,
        ///         "NomeCidade": "Campinas",
        ///         "id_estado": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da cidade a ser atualizada.</param>
        /// <param name="cidade">Dados atualizados da cidade.</param>
        /// <returns>Sem conteúdo em caso de sucesso.</returns>
        /// <response code="204">Cidade atualizada com sucesso.</response>
        /// <response code="400">Dados inválidos ou ID incorreto.</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpPut("/cidades/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Cidade cidade)
        {
            if (id != cidade.id_cidade)
            {
                return BadRequest(new { message = "Id da cidade incorreto!" });
            }
            _context.Entry(cidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove uma cidade pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /cidades/1
        ///
        /// </remarks>
        /// <param name="id">ID da cidade a ser removida.</param>
        /// <returns>Sem conteúdo em caso de sucesso.</returns>
        /// <response code="204">Cidade removida com sucesso.</response>
        /// <response code="404">Cidade não encontrada.</response>
        [HttpDelete("/cidades/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound(new { message = "Cidade não encontrada" });
            }
            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
