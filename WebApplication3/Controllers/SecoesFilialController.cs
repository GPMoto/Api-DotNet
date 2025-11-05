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
    public class SecoesFilialController : ControllerBase
    {

        private readonly SecoesFilialService secoesFilialService;

        public SecoesFilialController(SecoesFilialService context)
        {
            secoesFilialService = context;
        }

        /// <summary>
        /// Obtém uma lista de todas as seções de filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /secoesfilial
        ///
        /// </remarks>
        /// <returns>Uma lista de seções de filial</returns>
        /// <response code="200">Retorna a lista completa de seções de filial</response>
        [HttpGet("/secoesfilial")]
        [ProducesResponseType(typeof(IEnumerable<SecoesFilial>), 200)]
        public async Task<ActionResult<IEnumerable<SecoesFilial>>> Get()
        {
            return Ok(await secoesFilialService.GetAllAsync());

        }

        /// <summary>
        /// Obtém uma seção de filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /secoesfilial/1
        ///
        /// </remarks>
        /// <param name="id">ID da seção de filial</param>
        /// <returns>Dados da seção de filial</returns>
        /// <response code="200">Retorna a seção de filial encontrada</response>
        /// <response code="404">Seção de filial não encontrada</response>
        [HttpGet("/secoesfilial/{id}")]
        [ProducesResponseType(typeof(SecoesFilial), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SecoesFilial>> GetById(int id)
        {
            var SecoesFilial = await secoesFilialService.GetByIdAsync(id);
            if (SecoesFilial == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            return Ok(SecoesFilial);

        }

        /// <summary>
        /// Obtém seções de filial por ID da filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /secoesfilial/filial/1
        ///
        /// </remarks>
        /// <param name="id">ID da filial</param>
        /// <returns>Lista de seções de filial</returns>
        /// <response code="200">Retorna as seções de filial encontradas</response>
        /// <response code="404">Seção de filial não encontrada</response>
        [HttpGet("/secoesfilial/filial/{id}")]
        [ProducesResponseType(typeof(IEnumerable<SecoesFilial>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<SecoesFilial>>> GetByIdFilial(int id)
        {
            var secoesFilialAchadas = await secoesFilialService.GetByIdFilial(id);
            if (secoesFilialAchadas == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            return Ok(secoesFilialAchadas);

        }

        /// <summary>
        /// Cria uma nova seção de filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /secoesfilial
        ///     {
        ///         "id_secao": 1,
        ///         "Lado1": 100,
        ///         "Lado2": 200,
        ///         "Lado3": 150,
        ///         "Lado4": 120,
        ///         "id_tipo_secao": 2,
        ///         "id_filial": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="SecoesFilial">Dados da seção de filial</param>
        /// <returns>Seção de filial criada</returns>
        /// <response code="201">Seção de filial criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/secoesfilial")]
        [ProducesResponseType(typeof(SecoesFilial), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SecoesFilial>> Post([FromBody] SecoesFilial SecoesFilial)
        {
            try
            {
                if (SecoesFilial == null)
                {
                    return BadRequest(new { message = "Seção Filial não pode ser nula" });
                }
                if (SecoesFilial.Lado4 >10000 || SecoesFilial.Lado4 <= 0)
                {
                    throw new TamanhoInvalidoException(10000,1,"lado");
                }
                if (SecoesFilial.Lado1 > 10000 || SecoesFilial.Lado1 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado2 > 10000 || SecoesFilial.Lado2 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado3 > 10000 || SecoesFilial.Lado3 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                var createdSecao = await secoesFilialService.AddAsync(SecoesFilial);
                return CreatedAtAction(nameof(GetById), new { id = createdSecao.id_secao }, createdSecao);

            }
            catch(TamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma seção de filial existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /secoesfilial/1
        ///     {
        ///         "id_secao": 1,
        ///         "Lado1": 110,
        ///         "Lado2": 210,
        ///         "Lado3": 160,
        ///         "Lado4": 130,
        ///         "id_tipo_secao": 2,
        ///         "id_filial": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da seção de filial a ser atualizada</param>
        /// <param name="SecoesFilial">Dados atualizados da seção de filial</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Seção de filial atualizada com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Seção de filial não encontrada</response>
        [HttpPut("/secoesfilial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] SecoesFilial SecoesFilial)
        {
            try
            {
                if (id != SecoesFilial.id_secao)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id da seção filial incorreto!" });
                }
                if (SecoesFilial.Lado4 > 10000 || SecoesFilial.Lado4 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado1 > 10000 || SecoesFilial.Lado1 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado2 > 10000 || SecoesFilial.Lado2 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado3 > 10000 || SecoesFilial.Lado3 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                var existingSecoesFilial = await secoesFilialService.GetByIdAsync(id);
                if (existingSecoesFilial == null)
                {
                    return NotFound(new { message = "Seção Filial não encontrada" });
                }
                if (id != SecoesFilial.id_secao)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id da seção filial incorreto!" });
                }
                await secoesFilialService.UpdateAsync(SecoesFilial);
                return NoContent();

            }
            catch (TamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Remove uma seção de filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /secoesfilial/1
        ///
        /// </remarks>
        /// <param name="id">ID da seção de filial a ser removida</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Seção de filial removida com sucesso</response>
        /// <response code="404">Seção de filial não encontrada</response>
        [HttpDelete("/secoesfilial/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var existingSecoesFilial = await secoesFilialService.GetByIdAsync(id);
            if (existingSecoesFilial == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            var result = await secoesFilialService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            return NoContent();

        }
    }
}
