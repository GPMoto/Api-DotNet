using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilialController : ControllerBase
    {

        private readonly AppDbContext _context;

        public FilialController(AppDbContext context)
        {
            _context = context;
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
            var filiais = await _context.Filial.ToListAsync();
            if (filiais == null)
            {
                return NotFound(new { message = "Filiais não encontrada" });
            }
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
            var filial = await _context.Filial.FindAsync(id);
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
            var filial = await _context.Filial.Where(f => f.Cnpj == cnpj).ToListAsync();
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return Ok(filial);
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
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            _context.Filial.Add(filial);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = filial.id_filial }, filial);
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
            if(id != filial.id_filial)
            {
                return NotFound(new { StatusCode=400, message = "Id da filial não está correto" });
            }
            _context.Entry(filial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
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
            var filial = await _context.Filial.FindAsync(id);
            if(filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            _context.Filial.Remove(filial);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
