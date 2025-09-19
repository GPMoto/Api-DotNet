using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os estados.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /estados
        ///
        /// </remarks>
        /// <returns>Uma lista de estados</returns>
        /// <response code="200">Retorna a lista completa de estados</response>
        [HttpGet("/estados")]
        [ProducesResponseType(typeof(IEnumerable<Estado>), 200)]
        public async Task<ActionResult<IEnumerable<Estado>>> Get()
        {
            return await _context.Estado.ToListAsync();
        }

        /// <summary>
        /// Obtém um estado pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /estados/1
        ///
        /// </remarks>
        /// <param name="id">ID do estado</param>
        /// <returns>Dados do estado</returns>
        /// <response code="200">Retorna o estado encontrado</response>
        /// <response code="404">Estado não encontrado</response>
        [HttpGet("/estados/{id}")]
        [ProducesResponseType(typeof(Estado), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Estado>> GetById(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound(new { message = "Estado não encontrado" });
            }
            return Ok(estado);
        }

        /// <summary>
        /// Cria um novo estado.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /estados
        ///     {
        ///         "id_estado": 1,
        ///         "NomeEstado": "São Paulo",
        ///         "id_pais": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="estado">Dados do estado</param>
        /// <returns>Estado criado</returns>
        /// <response code="201">Estado criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/estados")]
        [ProducesResponseType(typeof(Estado), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Estado>> Post([FromBody] Estado estado)
        {
            if (estado == null)
            {
                return BadRequest(new { message = "Estado não pode ser nulo" });
            }
            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = estado.id_estado }, estado);
        }

        /// <summary>
        /// Atualiza os dados de um estado existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /estados/1
        ///     {
        ///         "id_estado": 1,
        ///         "NomeEstado": "Rio de Janeiro",
        ///         "id_pais": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do estado a ser atualizado</param>
        /// <param name="estado">Dados atualizados do estado</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Estado atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Estado não encontrado</response>
        [HttpPut("/estados/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Estado estado)
        {
            if (id != estado.id_estado)
            {
                return BadRequest(new {StatusCode=400, message = "Id do estado incorreto!" });
            }
            _context.Entry(estado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove um estado pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /estados/1
        ///
        /// </remarks>
        /// <param name="id">ID do estado a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Estado removido com sucesso</response>
        /// <response code="404">Estado não encontrado</response>
        [HttpDelete("/estados/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound(new { message = "Estado não encontrado" });
            }
            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
