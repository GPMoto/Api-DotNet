using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PaisController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/paises")]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            return await _context.Pais.ToListAsync();
        }

        [HttpGet("/paises/{id}")]
        public async Task<ActionResult<Pais>> GetById(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            return Ok(pais);
        }

        [HttpPost("/paises")]
        public async Task<ActionResult<Pais>> Post([FromBody] Pais pais)
        {
            if (pais == null)
            {
                return BadRequest(new { message = "Pais não pode ser nulo" });
            }
            _context.Pais.Add(pais);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pais.Id_pais }, pais);
        }

        [HttpPut("/paises/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pais pais)
        {
            if (id != pais.Id_pais)
            {
                return BadRequest(new {StatusCode=400, message = "Id do pais incorreto!" });
            }
            _context.Entry(pais).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/paises/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound(new { message = "Pais não encontrado" });
            }
            _context.Pais.Remove(pais);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
