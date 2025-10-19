using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly PaisService paisService;
        public PaisController(PaisService context)
        {
            paisService = context;
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
            return Ok(await paisService.GetAllAsync());
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
            var pais = await paisService.GetByIdAsync(id);
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
            try 
            {
                var createdPais = await paisService.AddAsync(pais);
                return CreatedAtAction(nameof(GetById), new { id = createdPais.Id_pais }, createdPais);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }

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
            var existingPais = await paisService.GetByIdAsync(id);
            if (existingPais == null)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            return Ok(existingPais);

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
            var result = await paisService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            return NoContent();

        }
    }
}
