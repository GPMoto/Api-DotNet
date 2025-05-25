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

        [HttpGet("/motos")]
        public async Task<ActionResult<IEnumerable<Moto>>> Get()
        {
            return await _context.Moto.ToListAsync();
        }

     
        [HttpGet("/motos/{id}")]
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
        /// Busca moto pelo identificador dela, seja placa, chassi ou numero do motor
        /// </summary>
        /// <param name="identificador"></param>
        [HttpGet("/motos/identificador/{identificador}")]
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
        /// Acha motos que estão cadastradas em uma filial especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/motos/filial/{id}")]
        public async Task<ActionResult<IEnumerable<Moto>>> GetByIdFilial(int id)
        {
            var motos = await _context.Moto.Where(m => m.id_filial == id).ToListAsync();
            if(motos == null )
            {
                return NotFound(new { message = "Motos não encontrada" });
            }
            return Ok(motos);
        }

        [HttpPost("/motos")]
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

        [HttpPut("/motos/{id}")]
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

        [HttpDelete("/motos/{id}")]
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
