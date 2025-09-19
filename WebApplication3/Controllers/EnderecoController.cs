using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os endereços.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /enderecos
        ///
        /// </remarks>
        /// <returns>Uma lista de endereços</returns>
        /// <response code="200">Retorna a lista completa de endereços</response>
        [HttpGet("/enderecos")]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), 200)]
        public async Task<ActionResult<IEnumerable<Endereco>>> Get()
        {
            return await _context.Endereco.ToListAsync();
        }
        
        /// <summary>
        /// Obtém um endereço pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /enderecos/1
        ///
        /// </remarks>
        /// <param name="id">ID do endereço</param>
        /// <returns>Dados do endereço</returns>
        /// <response code="200">Retorna o endereço encontrado</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpGet("/enderecos/{id}")]
        [ProducesResponseType(typeof(Endereco), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Endereco>> GetById(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            return Ok(endereco);
        }

        /// <summary>
        /// Obtém endereços pelo CEP.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /enderecos/cep/12345678
        ///
        /// </remarks>
        /// <param name="cep">CEP do endereço</param>
        /// <returns>Lista de endereços encontrados</returns>
        /// <response code="200">Retorna os endereços encontrados</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpGet("/enderecos/cep/{cep}")]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Endereco>> GetByCep(string cep)
        {
            var endereco = await _context.Endereco.Where(e => e.Cep == cep).ToListAsync();
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            return Ok(endereco);
        }


        /// <summary>
        /// Cria um novo endereço.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /enderecos
        ///     {
        ///         "id_endereco": 1,
        ///         "NomeLogradouro": "Rua das Flores",
        ///         "NumeroLogradouro": "123",
        ///         "Cep": "12345678",
        ///         "id_cidade": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="endereco">Dados do endereço</param>
        /// <returns>Endereço criado</returns>
        /// <response code="201">Endereço criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/enderecos")]
        [ProducesResponseType(typeof(Endereco), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Endereco>> Post([FromBody] Endereco endereco)
        {
            try
            {
                if (endereco == null)
                {
                    return BadRequest(new { message = "Endereco não pode ser nulo" });
                }
                if (endereco.Cep.Length > 8)
                {
                    throw new CepTamanhoInvalidoException();
                }
                _context.Endereco.Add(endereco);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = endereco.id_endereco }, endereco);
            }catch(CepTamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /enderecos/1
        ///     {
        ///         "id_endereco": 1,
        ///         "NomeLogradouro": "Rua das Flores",
        ///         "NumeroLogradouro": "123",
        ///         "Cep": "12345678",
        ///         "id_cidade": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do endereço a ser atualizado</param>
        /// <param name="endereco">Dados atualizados do endereço</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Endereço atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpPut("/enderecos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Endereco endereco)
        {
            try
            {
                if (id != endereco.id_endereco)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do endereco incorreto!" });
                }
                if (endereco.Cep.Length > 8)
                {
                    throw new CepTamanhoInvalidoException();
                }
                _context.Entry(endereco).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (CepTamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        /// <summary>
        /// Remove um endereço pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /enderecos/1
        ///
        /// </remarks>
        /// <param name="id">ID do endereço a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Endereço removido com sucesso</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpDelete("/enderecos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
