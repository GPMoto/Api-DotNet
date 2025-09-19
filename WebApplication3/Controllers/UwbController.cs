using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UwbController : ControllerBase
    {

        private readonly AppDbContext _context;

        public UwbController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os UWBs.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /uwb
        ///
        /// </remarks>
        /// <returns>Uma lista de UWBs</returns>
        /// <response code="200">Retorna a lista completa de UWBs</response>
        [HttpGet("/uwb")]
        [ProducesResponseType(typeof(IEnumerable<Uwb>), 200)]
        public async Task<ActionResult<IEnumerable<Uwb>>> Get()
        {
            return await _context.Uwb.ToListAsync();
        }

        /// <summary>
        /// Obtém um UWB pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /uwb/1
        ///
        /// </remarks>
        /// <param name="id">ID do UWB</param>
        /// <returns>Dados do UWB</returns>
        /// <response code="200">Retorna o UWB encontrado</response>
        /// <response code="404">UWB não encontrado</response>
        [HttpGet("/uwb/{id}")]
        [ProducesResponseType(typeof(Uwb), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Uwb>> GetById(int id)
        {
            var uwb = await _context.Uwb.FindAsync(id);
            if (uwb == null)
            {
                return NotFound(new { message = "UWB não encontrado" });
            }
            return Ok(uwb);
        }

        /// <summary>
        /// Cria um novo UWB.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /uwb
        ///     {
        ///         "id_uwb": 1,
        ///         "id_moto": 2,
        ///         "ValorUwb": "UWB123456"
        ///     }
        ///
        /// </remarks>
        /// <param name="uwb">Dados do UWB</param>
        /// <returns>UWB criado</returns>
        /// <response code="201">UWB criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/uwb")]
        [ProducesResponseType(typeof(Uwb), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Uwb>> Post([FromBody] Uwb uwb)
        {
            if (uwb == null)
            {
                return BadRequest(new { message = "UWB não pode ser nulo" });
            }
            _context.Uwb.Add(uwb);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = uwb.id_uwb }, uwb);
        }

        /// <summary>
        /// Atualiza os dados de um UWB existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /uwb/1
        ///     {
        ///         "id_uwb": 1,
        ///         "id_moto": 2,
        ///         "ValorUwb": "UWB654321"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do UWB a ser atualizado</param>
        /// <param name="uwb">Dados atualizados do UWB</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">UWB atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">UWB não encontrado</response>
        [HttpPut("/uwb/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Uwb uwb)
        {
            if (id != uwb.id_uwb)
            {
                return BadRequest(new { message = "Id do UWB incorreto!" });
            }
            _context.Entry(uwb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove um UWB pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /uwb/1
        ///
        /// </remarks>
        /// <param name="id">ID do UWB a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">UWB removido com sucesso</response>
        /// <response code="404">UWB não encontrado</response>
        [HttpDelete("/uwb/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var uwb = await _context.Uwb.FindAsync(id);
            if (uwb == null)
            {
                return NotFound(new { message = "UWB não encontrado" });
            }
            _context.Uwb.Remove(uwb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
