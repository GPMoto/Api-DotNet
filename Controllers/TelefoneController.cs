using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TelefoneController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/telefones")]
        public async Task<ActionResult<IEnumerable<Telefone>>> Get()
        {
            return await _context.Telefone.ToListAsync();
        }

        [HttpGet("/telefones/{id}")]
        public async Task<ActionResult<Telefone>> GetById(int id)
        {
            var telefone = await _context.Telefone.FindAsync(id);
            if (telefone == null)
            {
                return NotFound(new { message = "Telefone não encontrado" });
            }
            return Ok(telefone);
        }

        [HttpPost("/telefones")]
        public async Task<ActionResult<Telefone>> Post([FromBody] Telefone telefone)
        {
            if (telefone == null)
            {
                return BadRequest(new { message = "Telefone não pode ser nulo" });
            }
            _context.Telefone.Add(telefone);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = telefone.id_telefone }, telefone);
        }

        [HttpPut("/telefones/{id}")]
        public async Task<ActionResult<Telefone>> Put(int id, [FromBody] Telefone telefone)
        {
            if (id != telefone.id_telefone)
            {
                return BadRequest(new { message = "Id do telefone incorreto!" });
            }
            _context.Entry(telefone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/telefones/{id}")]
        public async Task<ActionResult<Telefone>> Delete(int id)
        {
            var telefone = await _context.Telefone.FindAsync(id);
            if (telefone == null)
            {
                return NotFound(new { message = "Telefone não encontrado" });
            }
            _context.Telefone.Remove(telefone);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
