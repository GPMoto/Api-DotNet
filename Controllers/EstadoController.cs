using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/estados")]
        public async Task<ActionResult<IEnumerable<Estado>>> Get()
        {
            return await _context.Estado.ToListAsync();
        }

        [HttpGet("/estados/{id}")]
        public async Task<ActionResult<Estado>> GetById(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound(new { message = "Estado não encontrado" });
            }
            return Ok(estado);
        }

        [HttpPost("/estados")]
        public async Task<ActionResult<Estado>> Post([FromBody] Estado estado)
        {
            if (estado == null)
            {
                return BadRequest(new { message = "Estado não pode ser nulo" });
            }
            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estado.id_estado }, estado);
        }

        [HttpPut("/estados/{id}")]
        public async Task<ActionResult<Estado>> Put(int id, [FromBody] Estado estado)
        {
            if (id != estado.id_estado)
            {
                return BadRequest(new { message = "Id do estado incorreto!" });
            }
            _context.Entry(estado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/estados/{id}")]
        public async Task<ActionResult<Estado>> Delete(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound(new { message = "Estado não encontrado" });
            }
            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
