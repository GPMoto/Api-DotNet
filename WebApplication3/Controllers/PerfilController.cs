using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerfilController : ControllerBase
    { 

        private readonly PerfilService perfilService;

        public PerfilController(PerfilService context)
        {
            perfilService = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os perfis.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /perfis
        ///
        /// </remarks>
        /// <returns>Uma lista de perfis</returns>
        /// <response code="200">Retorna a lista completa de perfis</response>
        [HttpGet("/perfis")]
        [ProducesResponseType(typeof(IEnumerable<Perfil>), 200)]
        public async Task<ActionResult<IEnumerable<Perfil>>> Get()
        {
            return Ok(await perfilService.GetAllPerfisAsync());

        }

        /// <summary>
        /// Obtém um perfil pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /perfis/1
        ///
        /// </remarks>
        /// <param name="id">ID do perfil</param>
        /// <returns>Dados do perfil</returns>
        /// <response code="200">Retorna o perfil encontrado</response>
        /// <response code="404">Perfil não encontrado</response>
        [HttpGet("/perfis/{id}")]
        [ProducesResponseType(typeof(Perfil), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Perfil>> GetById(int id)
        {
            var perfil = await perfilService.GetPerfilByIdAsync(id);
            if (perfil == null)
            {
                return NotFound(new { message = "Perfil não encontrado" });
            }
            return Ok(perfil);

        }

        /// <summary>
        /// Cria um novo perfil.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /perfis
        ///     {
        ///         "id_perfil": 1,
        ///         "NomePerfil": "Administrador"
        ///     }
        ///
        /// </remarks>
        /// <param name="perfil">Dados do perfil</param>
        /// <returns>Perfil criado</returns>
        /// <response code="201">Perfil criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/perfis")]
        [ProducesResponseType(typeof(Perfil), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Perfil>> Post([FromBody] Perfil perfil)
        {
            try 
            {
                var createdPerfil = await perfilService.CreatePerfilAsync(perfil);
                return CreatedAtAction(nameof(GetById), new { id = createdPerfil.id_perfil }, createdPerfil);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }

        }

        /// <summary>
        /// Atualiza os dados de um perfil existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /perfis/1
        ///     {
        ///         "id_perfil": 1,
        ///         "NomePerfil": "Usuário"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do perfil a ser atualizado</param>
        /// <param name="perfil">Dados atualizados do perfil</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Perfil atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Perfil não encontrado</response>
        [HttpPut("/perfis/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Perfil perfil)
        {
            try
            {
                if(id != perfil.id_perfil)
                {
                    return BadRequest(new { message = "ID do perfil incorreto" });
                }
                await perfilService.UpdatePerfilAsync(perfil,id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, message = ex.Message });
            }

        }

        /// <summary>
        /// Remove um perfil pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /perfis/1
        ///
        /// </remarks>
        /// <param name="id">ID do perfil a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Perfil removido com sucesso</response>
        /// <response code="404">Perfil não encontrado</response>
        [HttpDelete("/perfis/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await perfilService.DeletePerfilAsync(id);
                if(!result)
                {
                    return NotFound(new { message = "Perfil não encontrado" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }


        }
    }
}
