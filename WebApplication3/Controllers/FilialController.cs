using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilialController : ControllerBase
    {

        private readonly FilialService filialService;

        public FilialController(FilialService filialService)
        {
            this.filialService = filialService;
        }

        /// <summary>
        /// Obtém uma lista de todas as filiais.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /filiais
        ///
        /// </remarks>
        /// <returns>Uma lista de filiais</returns>
        /// <response code="200">Retorna a lista completa de filiais</response>
        [HttpGet("/filiais")]
        [ProducesResponseType(typeof(IEnumerable<Filial>), 200)]
        public async Task<ActionResult<IEnumerable<Filial>>> Get()
        {
            var filiais = await filialService.GetAllAsync();
            return Ok(filiais);

        }

        /// <summary>
        /// Obtém uma filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /filiais/1
        ///
        /// </remarks>
        /// <param name="id">ID da filial</param>
        /// <returns>Dados da filial</returns>
        /// <response code="200">Retorna a filial encontrada</response>
        /// <response code="404">Filial não encontrada</response>
        [HttpGet("/filiais/{id}")]
        [ProducesResponseType(typeof(Filial), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Filial>> GetById(int id)
        {
            var filial = await filialService.GetByIdAsync(id);
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return Ok(filial);

        }

        /// <summary>
        /// Obtém filiais pelo CNPJ.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /filiais/cnpj/12345678000199
        ///
        /// </remarks>
        /// <param name="cnpj">CNPJ da filial</param>
        /// <returns>Lista de filiais encontradas</returns>
        /// <response code="200">Retorna as filiais encontradas</response>
        /// <response code="404">Filial não encontrada</response>
        [HttpGet("/filiais/cnpj/{cnpj}")]
        [ProducesResponseType(typeof(IEnumerable<Filial>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Filial>> GetByCnph(string cnpj)
        {
            var filiais = await filialService.getByCNPJ(cnpj);
            if (filiais == null || !filiais.Any())
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return Ok(filiais);

        }

        /// <summary>
        /// Cria uma nova filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /filiais
        ///     {
        ///         "id_filial": 1,
        ///         "Cnpj": "12345678000199",
        ///         "senha": "senha123",
        ///         "id_endereco": 2,
        ///         "id_contato": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="filial">Dados da filial</param>
        /// <returns>Filial criada</returns>
        /// <response code="201">Filial criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/filiais")]
        [ProducesResponseType(typeof(Filial), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Filial>> Post([FromBody] Filial filial)
        {
            try 
            {
                var createdFilial = await filialService.AddAsync(filial);
                return CreatedAtAction(nameof(GetById), new { id = createdFilial.id_filial }, createdFilial);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma filial existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /filiais/1
        ///     {
        ///         "id_filial": 1,
        ///         "Cnpj": "12345678000199",
        ///         "senha": "novaSenha",
        ///         "id_endereco": 2,
        ///         "id_contato": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da filial a ser atualizada</param>
        /// <param name="filial">Dados atualizados da filial</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Filial atualizada com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Filial não encontrada</response>
        [HttpPut("/filiais/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Filial filial)
        {
            try 
            {
                var result = await filialService.UpdateAsync(id, filial);
                if (!result)
                {
                    return NotFound(new { message = "Filial não encontrada" });
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma filial pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /filiais/1
        ///
        /// </remarks>
        /// <param name="id">ID da filial a ser removida</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Filial removida com sucesso</response>
        /// <response code="404">Filial não encontrada</response>
        [HttpDelete("/filiais/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await filialService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return NoContent();

        }

    }
}
