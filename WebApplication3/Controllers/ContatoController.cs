using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {


        private readonly AppDbContext _context;

        public ContatoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os contatos cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /contatos
        ///
        /// </remarks>
        /// <returns>Lista de contatos.</returns>
        /// <response code="200">Retorna a lista de contatos.</response>
        [HttpGet("/contatos")]
        [ProducesResponseType(typeof(IEnumerable<Contato>), 200)]
        public async Task<ActionResult<IEnumerable<Contato>>> Get()
        {
            return await _context.Contato.ToListAsync();
        }

        /// <summary>
        /// Retorna um contato pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /contatos/1
        ///
        /// </remarks>
        /// <param name="id">ID do contato.</param>
        /// <returns>Dados do contato.</returns>
        /// <response code="200">Retorna o contato encontrado.</response>
        /// <response code="404">Contato não encontrado.</response>
        [HttpGet("/contatos/{id}")]
        [ProducesResponseType(typeof(Contato), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Contato>> GetById(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            return Ok(contato);
        }

        /// <summary>
        /// Retorna contatos pelo nome do dono.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /contatos/nomeDono/João
        ///
        /// </remarks>
        /// <param name="nome">Nome do dono do contato.</param>
        /// <returns>Lista de contatos encontrados.</returns>
        /// <response code="200">Retorna os contatos encontrados.</response>
        /// <response code="404">Contato não encontrado.</response>
        [HttpGet("/contatos/nomeDono/{nome}")]
        [ProducesResponseType(typeof(IEnumerable<Contato>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Contato>> GetByNameDono(string nome)
        {
            var contato = await _context.Contato.Where(c => c.nmDono.Contains(nome)).ToListAsync();
            if(contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            return Ok(contato);
        }


        /// <summary>
        /// Cria um novo contato.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /contatos
        ///     {
        ///         "id_contato": 1,
        ///         "nmDono": "João Silva",
        ///         "status": 1,
        ///         "id_Telefone": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="contato">Dados do contato.</param>
        /// <returns>Contato criado.</returns>
        /// <response code="201">Contato criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("/contatos")]
        [ProducesResponseType(typeof(Contato), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Contato>> Post([FromBody] Contato contato)
        {
            try
            {
                if (contato == null)
                {
                    return BadRequest(new { message = "Contato não pode ser nulo" });
                }
                if (contato.status != 0 || contato.status != 1)
                {
                    throw new StatusInvalidoException();
                }
                _context.Contato.Add(contato);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = contato.id_contato }, contato);
            }
            catch (StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um contato existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /contatos/1
        ///     {
        ///         "id_contato": 1,
        ///         "nmDono": "João Silva",
        ///         "status": 0,
        ///         "id_Telefone": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do contato a ser atualizado.</param>
        /// <param name="contato">Dados atualizados do contato.</param>
        /// <returns>Sem conteúdo em caso de sucesso.</returns>
        /// <response code="204">Contato atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos ou ID incorreto.</response>
        /// <response code="404">Contato não encontrado.</response>
        [HttpPut("/contatos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Contato contato)
        {
            try
            {
                if (id != contato.id_contato)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do contato incorreto!" });
                }
                if (contato.status != 0 || contato.status != 1)
                {
                    throw new StatusInvalidoException();
                }
                _context.Entry(contato).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }


        /// <summary>
        /// Remove um contato pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /contatos/1
        ///
        /// </remarks>
        /// <param name="id">ID do contato a ser removido.</param>
        /// <returns>Sem conteúdo em caso de sucesso.</returns>
        /// <response code="204">Contato removido com sucesso.</response>
        /// <response code="404">Contato não encontrado.</response>
        [HttpDelete("/contatos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
