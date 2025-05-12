using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
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

        [HttpGet("/motos/{identificador}")]
        public async Task<ActionResult<Moto>> GetByIdentificador(string identificador)
        {
            var moto = await _context.Moto.Where(m=> m.IdentificadorMoto.Contains(identificador)).ToListAsync();
            if (moto == null)
            {
                return NotFound(new { message = "Moto não encontrada" });
            }
            return Ok(moto);
        }

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
        public async Task<ActionResult<Moto>> Post(Moto moto)
        {
            if (moto == null)
            {
                return BadRequest(new { message = "Moto não pode ser nula" });
            }
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = moto.id_moto }, moto);
        }

        [HttpPut("/motos/{id}")]
        public async Task<ActionResult<Moto>> Put(int id, Moto moto)
        {
            if (id != moto.id_moto)
            {
                return BadRequest(new { message = "ID da moto não confere" });
            }
            _context.Entry(moto).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("/motos/{id}")]
        public async Task<ActionResult<Moto>> Delete(int id)
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
