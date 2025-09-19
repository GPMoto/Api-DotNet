using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MotoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todas as motos.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /motos
        ///
        /// </remarks>
        /// <returns>Uma lista de motos</returns>
        /// <response code="200">Retorna a lista completa de motos</response>
        [HttpGet("/motos")]
        [ProducesResponseType(typeof(IEnumerable<Moto>), 200)]
        public async Task<ActionResult<IEnumerable<Moto>>> Get()
        {
            return await _context.Moto.ToListAsync();
        }

     
        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /motos/1
        ///
        /// </remarks>
        /// <param name="id">ID da moto</param>
        /// <returns>Dados da moto</returns>
        /// <response code="200">Retorna a moto encontrada</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpGet("/motos/{id}")]
        [ProducesResponseType(typeof(Moto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null)
            {
                return NotFound(new {message = "Moto não encontrada"});
            }
            return Ok(moto);
        }

        /// <summary>
        /// Busca moto pelo identificador dela, seja placa, chassi ou número do motor.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /motos/identificador/ABC1234
        ///
        /// </remarks>
        /// <param name="identificador">Identificador da moto</param>
        /// <returns>Lista de motos encontradas</returns>
        /// <response code="200">Retorna as motos encontradas</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpGet("/motos/identificador/{identificador}")]
        [ProducesResponseType(typeof(IEnumerable<Moto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Moto>> GetByIdentificador(string identificador)
        {
            var moto = await _context.Moto.Where(m=> m.IdentificadorMoto == identificador).ToListAsync();
            if (moto == null)
            {
                return NotFound(new { message = "Moto não encontrada" });
            }
            return Ok(moto);
        }

        /// <summary>
        /// Obtém motos cadastradas em uma filial específica.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /motos/filial/1
        ///
        /// </remarks>
        /// <param name="id">ID da filial</param>
        /// <returns>Lista de motos da filial</returns>
        /// <response code="200">Retorna as motos da filial</response>
        /// <response code="404">Motos não encontradas</response>
        [HttpGet("/motos/filial/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Moto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Moto>>> GetByIdFilial(int id)
        {
            var motos = await _context.Moto.Where(m => m.id_filial == id).ToListAsync();
            if(motos == null )
            {
                return NotFound(new { message = "Motos não encontrada" });
            }
            return Ok(motos);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /motos
        ///     {
        ///         "id_moto": 1,
        ///         "Status": 1,
        ///         "CondicaoMoto": "Nova",
        ///         "IdentificadorMoto": "ABC1234",
        ///         "id_filial": 2,
        ///         "id_tipo_moto": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="moto">Dados da moto</param>
        /// <returns>Moto criada</returns>
        /// <response code="201">Moto criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/motos")]
        [ProducesResponseType(typeof(Moto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Moto>> Post([FromBody]Moto moto)
        {
            try
            {
                if (moto == null)
                {
                    return BadRequest(new {StatusCode=400, message = "Moto não pode ser nula" });
                }
                if(moto.Status != 0 && moto.Status != 1)
                {
                    Console.WriteLine(moto.Status);
                    throw new StatusInvalidoException();
                }
                _context.Moto.Add(moto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = moto.id_moto }, moto);
            }
            catch (StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /motos/1
        ///     {
        ///         "id_moto": 1,
        ///         "Status": 0,
        ///         "CondicaoMoto": "Usada",
        ///         "IdentificadorMoto": "ABC1234",
        ///         "id_filial": 2,
        ///         "id_tipo_moto": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da moto a ser atualizada</param>
        /// <param name="moto">Dados atualizados da moto</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Moto atualizada com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpPut("/motos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, [FromBody] Moto moto)
        {
            try
            {
                if (id != moto.id_moto)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id da moto incorreto" });
                }
                if (moto.Status != 0 && moto.Status != 1)
                {
                    Console.WriteLine(moto.Status);
                    throw new StatusInvalidoException();
                }
                _context.Entry(moto).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        /// <summary>
        /// Remove uma moto pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /motos/1
        ///
        /// </remarks>
        /// <param name="id">ID da moto a ser removida</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Moto removida com sucesso</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpDelete("/motos/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var moto = await _context.Moto.FindAsync(id);
            if (moto == null)
            {
                return NotFound(new { message = "Moto não encontrada" });
            }
            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
