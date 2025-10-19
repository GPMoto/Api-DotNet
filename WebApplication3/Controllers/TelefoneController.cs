using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {

        private readonly TelefoneService telefoneService;

        public TelefoneController(TelefoneService context)
        {
            telefoneService = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os telefones.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /telefones
        ///
        /// </remarks>
        /// <returns>Uma lista de telefones</returns>
        /// <response code="200">Retorna a lista completa de telefones</response>
        [HttpGet("/telefones")]
        public async Task<ActionResult<IEnumerable<Telefone>>> Get()
        {
            return Ok(await telefoneService.GetAllAsync());

        }

        /// <summary>
        /// Obtém um telefone pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /telefones/1
        ///
        /// </remarks>
        /// <param name="id">ID do telefone</param>
        /// <returns>Dados do telefone</returns>
        /// <response code="200">Retorna o telefone encontrado</response>
        /// <response code="404">Telefone não encontrado</response>
        [HttpGet("/telefones/{id}")]
        [ProducesResponseType(typeof(Telefone), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Telefone>> GetById(int id)
        {
            var telefone = await telefoneService.GetByIdAsync(id);
            if (telefone == null)
            {
                return NotFound(new { message = "Telefone não encontrado" });
            }
            return Ok(telefone);

        }

        /// <summary>
        /// Cria um novo telefone.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /telefones
        ///     {
        ///         "id_telefone": 1,
        ///         "Ddd": "011",
        ///         "Ddi": "055",
        ///         "Numero": "912345678"
        ///     }
        ///
        /// </remarks>
        /// <param name="telefone">Dados do telefone</param>
        /// <returns>Telefone criado</returns>
        /// <response code="201">Telefone criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/telefones")]
        public async Task<ActionResult<Telefone>> Post([FromBody] Telefone telefone)
        {
            try
            {
                if (telefone == null)
                {
                    return BadRequest(new { message = "Telefone não pode ser nulo" });
                }
                if(telefone.Ddd.Length != 3)
                {
                    throw new TamanhoInvalidoException(3, "telefone");
                }
                if (telefone.Ddi.Length != 3)
                {
                    throw new TamanhoInvalidoException(3, "telefone");
                }
                var createdTelefone = await telefoneService.CreateAsync(telefone);
                return CreatedAtAction(nameof(GetById), new { id = createdTelefone.id_telefone }, createdTelefone);

            }
            catch (TamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um telefone existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /telefones/1
        ///     {
        ///         "id_telefone": 1,
        ///         "Ddd": "021",
        ///         "Ddi": "055",
        ///         "Numero": "998877665"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do telefone a ser atualizado</param>
        /// <param name="telefone">Dados atualizados do telefone</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Telefone atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Telefone não encontrado</response>
        [HttpPut("/telefones/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Telefone telefone)
        {
            try
            {
                if (id != telefone.id_telefone)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do telefone incorreto!" });
                }
                if (telefone.Ddd.Length != 3)
                {
                    throw new TamanhoInvalidoException(3, "telefone");
                }
                if (telefone.Ddi.Length != 3)
                {
                    throw new TamanhoInvalidoException(3, "telefone");
                }
                var existingTelefone = await telefoneService.GetByIdAsync(id);
                if (existingTelefone == null)
                {
                    return NotFound(new { message = "Telefone não encontrado" });
                }
                if (id != telefone.id_telefone)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do telefone incorreto!" });
                }
                await telefoneService.UpdateAsync(telefone);
                return NoContent();

            }
            catch (TamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Remove um telefone pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /telefones/1
        ///
        /// </remarks>
        /// <param name="id">ID do telefone a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Telefone removido com sucesso</response>
        /// <response code="404">Telefone não encontrado</response>
        [HttpDelete("/telefones/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var existingTelefone = await telefoneService.GetByIdAsync(id);
            if (existingTelefone == null)
            {
                return NotFound(new { message = "Telefone não encontrado" });
            }
            await telefoneService.DeleteAsync(id);
            return NoContent();

        }
    }
}
