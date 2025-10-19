using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TipoSecaoFilialController : ControllerBase
    {
        private readonly TIpoSecaoFilialService tipoSecaoFilialService;

        public TipoSecaoFilialController(TIpoSecaoFilialService context)
        {
            tipoSecaoFilialService = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os tipos de seção de filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /tiposecaofilial
        ///
        /// </remarks>
        /// <returns>Uma lista de tipos de seção de filial</returns>
        /// <response code="200">Retorna a lista completa de tipos de seção de filial</response>
        [HttpGet("/tiposecaofilial")]
        [ProducesResponseType(typeof(IEnumerable<TipoSecao>), 200)]
        public async Task<ActionResult<IEnumerable<TipoSecao>>> Get()
        {
            return Ok(await tipoSecaoFilialService.GetAllAsync());

        }

        /// <summary>
        /// Obtém um tipo de seção de filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /tiposecaofilial/1
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de seção de filial</param>
        /// <returns>Dados do tipo de seção de filial</returns>
        /// <response code="200">Retorna o tipo de seção de filial encontrado</response>
        /// <response code="404">Tipo de seção de filial não encontrado</response>
        [HttpGet("/tiposecaofilial/{id}")]
        [ProducesResponseType(typeof(TipoSecao), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TipoSecao>> GetById(int id)
        {
            var tipoSecaoFilial = await tipoSecaoFilialService.GetByIdAsync(id);
            if (tipoSecaoFilial == null)
            {
                return NotFound(new { message = "Tipo de seção filial não encontrado" });
            }
            return Ok(tipoSecaoFilial);

        }

        /// <summary>
        /// Cria um novo tipo de seção de filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /tiposecaofilial
        ///     {
        ///         "id_tipo_secao": 1,
        ///         "NomeTipoSecao": "Coberta"
        ///     }
        ///
        /// </remarks>
        /// <param name="tipoSecaoFilial">Dados do tipo de seção de filial</param>
        /// <returns>Tipo de seção de filial criado</returns>
        /// <response code="201">Tipo de seção de filial criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/tiposecaofilial")]
        [ProducesResponseType(typeof(TipoSecao), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TipoSecao>> Post([FromBody] TipoSecao tipoSecaoFilial)
        {
            try 
            {
                var createdTipoSecaoFilial = await tipoSecaoFilialService.CreateAsync(tipoSecaoFilial);
                return CreatedAtAction(nameof(GetById), new { id = createdTipoSecaoFilial.id_tipo_secao }, createdTipoSecaoFilial);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }

        }

        /// <summary>
        /// Atualiza os dados de um tipo de seção de filial existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /tiposecaofilial/1
        ///     {
        ///         "id_tipo_secao": 1,
        ///         "NomeTipoSecao": "Descoberta"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de seção de filial a ser atualizado</param>
        /// <param name="tipoSecaoFilial">Dados atualizados do tipo de seção de filial</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Tipo de seção de filial atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Tipo de seção de filial não encontrado</response>
        [HttpPut("/tiposecaofilial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] TipoSecao tipoSecaoFilial)
        {
            try 
            {
                if (id != tipoSecaoFilial.id_tipo_secao)
                {
                    return BadRequest(new { message = "ID incorreto" });
                }
                var existingTipoSecaoFilial = await tipoSecaoFilialService.GetByIdAsync(id);
                if (existingTipoSecaoFilial == null)
                {
                    return NotFound(new { message = "Tipo de seção filial não encontrado" });
                }
                await tipoSecaoFilialService.UpdateAsync(tipoSecaoFilial);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um tipo de seção de filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /tiposecaofilial/1
        ///
        /// </remarks>
        /// <param name="id">ID do tipo de seção de filial a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Tipo de seção de filial removido com sucesso</response>
        /// <response code="404">Tipo de seção de filial não encontrado</response>
        [HttpDelete("/tiposecaofilial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
          
            var existingTipoSecaoFilial = await tipoSecaoFilialService.GetByIdAsync(id);
            if (existingTipoSecaoFilial == null)
            {
                return NotFound(new { message = "Tipo de seção filial não encontrado" });
            }
            var result = await tipoSecaoFilialService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Tipo de seção filial não encontrado" });
            }
            
            return NoContent();

        }
    }
}
