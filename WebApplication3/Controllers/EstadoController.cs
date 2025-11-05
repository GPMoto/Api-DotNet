using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {

        private readonly EstadoService estadoService;

        public EstadoController(EstadoService estadoService)
        {
            this.estadoService = estadoService;
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
            return Ok(await estadoService.GetAllEstadosAsync());
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
            var estado = await estadoService.GetEstadoByIdAsync(id);
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
            try 
            {
                var createdEstado =  await estadoService.AddEstadoAsync(estado);
                return CreatedAtAction(nameof(GetById), new { id = createdEstado.id_estado }, createdEstado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }
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
            try 
            {
                await estadoService.UpdateEstadoAsync(estado, id);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }
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
            try 
            {
                await estadoService.DeleteEstadoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
    }
}
