using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoMotoController : ControllerBase
    {
        private readonly TipoMotoService tipoMotoService;

        public TipoMotoController(TipoMotoService context)
        {
            tipoMotoService = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os tipos de moto.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /tipomotos
        ///
        /// </remarks>
        /// <returns>Uma lista de tipos de moto</returns>
        /// <response code="200">Retorna a lista completa de tipos de moto</response>
        [HttpGet("/tipomotos")]
        [ProducesResponseType(typeof(IEnumerable<TipoMoto>), 200)]
        public async Task<ActionResult<IEnumerable<TipoMoto>>> Get()
        {
            return Ok(await tipoMotoService.GetAllTipoMotoAsync());

        }


        /// <summary>
        /// Obtém um tipo de moto pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /tipomotos/1
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de moto</param>
        /// <returns>Dados do tipo de moto</returns>
        /// <response code="200">Retorna o tipo de moto encontrado</response>
        /// <response code="404">Tipo de moto não encontrado</response>
        [HttpGet("/tipomotos/{id}")]
        [ProducesResponseType(typeof(TipoMoto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TipoMoto>> GetById(int id)
        {
            var tipoMoto = await tipoMotoService.GetTipoMotoByIdAsync(id);
            if (tipoMoto == null)
            {
                return NotFound(new { message = "Tipo de moto não encontrado" });
            }
            return Ok(tipoMoto);

        }

        /// <summary>
        /// Cria um novo tipo de moto.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /tipomotos
        ///     {
        ///         "id_tipo_moto": 1,
        ///         "NomeTipoMoto": "Scooter"
        ///     }
        ///
        /// </remarks>
        /// <param name="tipoMoto">Dados do tipo de moto</param>
        /// <returns>Tipo de moto criado</returns>
        /// <response code="201">Tipo de moto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/tipomotos")]
        [ProducesResponseType(typeof(TipoMoto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TipoMoto>> Post([FromBody] TipoMoto tipoMoto)
        {
            try 
            {
                var createdTipoMoto = await tipoMotoService.CreateTipoMotoAsync(tipoMoto);
                return CreatedAtAction(nameof(GetById), new { id = createdTipoMoto.id_tipo_moto }, createdTipoMoto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }

        }

        /// <summary>
        /// Atualiza os dados de um tipo de moto existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /tipomotos/1
        ///     {
        ///         "id_tipo_moto": 1,
        ///         "NomeTipoMoto": "Custom"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de moto a ser atualizado</param>
        /// <param name="tipoMoto">Dados atualizados do tipo de moto</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Tipo de moto atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Tipo de moto não encontrado</response>
        [HttpPut("/tipomotos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] TipoMoto tipoMoto)
        {
            try 
            {
                if (id != tipoMoto.id_tipo_moto)
                {
                    return BadRequest(new { StatusCode = 400, message = "ID incorreto" });
                }
                var existingTipoMoto = await tipoMotoService.GetTipoMotoByIdAsync(id);
                if (existingTipoMoto == null)
                {
                    return NotFound(new { message = "Tipo de moto não encontrado" });
                }
                await tipoMotoService.UpdateTipoMotoAsync(tipoMoto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um tipo de moto pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /tipomotos/1
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de moto a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Tipo de moto removido com sucesso</response>
        /// <response code="404">Tipo de moto não encontrado</response>
        [HttpDelete("/tipomotos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await tipoMotoService.DeleteTipoMotoAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Tipo de moto não encontrado" });
            }
            return NoContent();


        }
    }
}
