using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PaisController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os países.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /paises
        ///
        /// </remarks>
        /// <returns>Uma lista de países</returns>
        /// <response code="200">Retorna a lista completa de países</response>
        [HttpGet("/paises")]
        [ProducesResponseType(typeof(IEnumerable<Pais>), 200)]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            return await _context.Pais.ToListAsync();
        }

        /// <summary>
        /// Obtém um país pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /paises/1
        ///
        /// </remarks>
        /// <param name="id">ID do país</param>
        /// <returns>Dados do país</returns>
        /// <response code="200">Retorna o país encontrado</response>
        /// <response code="404">País não encontrado</response>
        [HttpGet("/paises/{id}")]
        [ProducesResponseType(typeof(Pais), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pais>> GetById(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            return Ok(pais);
        }

        /// <summary>
        /// Cria um novo país.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /paises
        ///     {
        ///         "Id_pais": 1,
        ///         "NomePais": "Brasil"
        ///     }
        ///
        /// </remarks>
        /// <param name="pais">Dados do país</param>
        /// <returns>País criado</returns>
        /// <response code="201">País criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/paises")]
        [ProducesResponseType(typeof(Pais), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Pais>> Post([FromBody] Pais pais)
        {
            if (pais == null)
            {
                return BadRequest(new { message = "Pais não pode ser nulo" });
            }
            _context.Pais.Add(pais);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pais.Id_pais }, pais);
        }

        /// <summary>
        /// Atualiza os dados de um país existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /paises/1
        ///     {
        ///         "Id_pais": 1,
        ///         "NomePais": "Argentina"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do país a ser atualizado</param>
        /// <param name="pais">Dados atualizados do país</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">País atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">País não encontrado</response>
        [HttpPut("/paises/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Pais pais)
        {
            if (id != pais.Id_pais)
            {
                return BadRequest(new {StatusCode=400, message = "Id do pais incorreto!" });
            }
            _context.Entry(pais).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove um país pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /paises/1
        ///
        /// </remarks>
        /// <param name="id">ID do país a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">País removido com sucesso</response>
        /// <response code="404">País não encontrado</response>
        [HttpDelete("/paises/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            _context.Pais.Remove(pais);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
