using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoMotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoMotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/tipomotos")]
        public async Task<ActionResult<IEnumerable<TipoMoto>>> Get()
        {
            return await _context.TipoMoto.ToListAsync();
        }
        [HttpGet("/tipomotos/{id}")]
        public async Task<ActionResult<TipoMoto>> GetById(int id)
        {
            var tipoMoto = await _context.TipoMoto.FindAsync(id);
            if (tipoMoto == null)
            {
                return NotFound(new { message = "Tipo de moto não encontrado" });
            }
            return Ok(tipoMoto);
        }

        [HttpPost("/tipomotos")]
        public async Task<ActionResult<TipoMoto>> Post([FromBody] TipoMoto tipoMoto)
        {
            if (tipoMoto == null)
            {
                return BadRequest(new { message = "Tipo de moto não pode ser nulo" });
            }
            _context.TipoMoto.Add(tipoMoto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = tipoMoto.id_tipo_moto }, tipoMoto);
        }

        [HttpPut("/tipomotos/{id}")]
        public async Task<ActionResult<TipoMoto>> Put(int id, [FromBody] TipoMoto tipoMoto)
        {
            if (id != tipoMoto.id_tipo_moto)
            {
                return BadRequest(new { message = "Id do tipo de moto incorreto!" });
            }
            _context.Entry(tipoMoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/tipomotos/{id}")]
        public async Task<ActionResult<TipoMoto>> Delete(int id)
        {
            var tipoMoto = await _context.TipoMoto.FindAsync(id);
            if (tipoMoto == null)
            {
                return NotFound(new { message = "Tipo de moto não encontrado" });
            }
            _context.TipoMoto.Remove(tipoMoto);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
