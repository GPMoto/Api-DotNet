using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerfilController : ControllerBase
    { 

        private readonly AppDbContext _context;

        public PerfilController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/perfis")]
        public async Task<ActionResult<IEnumerable<Perfil>>> Get()
        {
            return await _context.Perfil.ToListAsync();
        }

        [HttpGet("/perfis/{id}")]
        public async Task<ActionResult<Perfil>> GetById(int id)
        {
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return NotFound(new { message = "Perfil não encontrado" });
            }
            return Ok(perfil);
        }

        [HttpPost("/perfis")]
        public async Task<ActionResult<Perfil>> Post([FromBody] Perfil perfil)
        {
            if (perfil == null)
            {
                return BadRequest(new { message = "Perfil não pode ser nulo" });
            }
            _context.Perfil.Add(perfil);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = perfil.Id_perfil }, perfil);
        }

        [HttpPut("/perfis/{id}")]
        public async Task<ActionResult<Perfil>> Put(int id, [FromBody] Perfil perfil)
        {
            if (id != perfil.Id_perfil)
            {
                return BadRequest(new { message = "ID do perfil incorreto!" });
            }
            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/perfis/{id}")]
        public async Task<ActionResult<Perfil>> Delete(int id)
        {
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return NotFound(new { message = "Perfil não encontrado" });
            }
            _context.Perfil.Remove(perfil);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
