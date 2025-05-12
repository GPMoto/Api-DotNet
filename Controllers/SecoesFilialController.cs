using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SecoesFilialController : ControllerBase
    {

        private readonly AppDbContext _context;

        public SecoesFilialController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/secoesfilial")]
        public async Task<ActionResult<IEnumerable<SecoesFilial>>> Get()
        {
            return await _context.SecoesFilial.ToListAsync();
        }

        [HttpGet("/secoesfilial/{id}")]
        public async Task<ActionResult<SecoesFilial>> GetById(int id)
        {
            var SecoesFilial = await _context.SecoesFilial.FindAsync(id);
            if (SecoesFilial == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            return Ok(SecoesFilial);
        }

        [HttpGet("/secoesfilial/filial/{id}")]
        public async Task<ActionResult<IEnumerable<SecoesFilial>>> GetByIdFilial(int id)
        {
            var SecoesFilial = await _context.SecoesFilial.Where(s => s.id_filial == id).ToListAsync();
            if (SecoesFilial == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            return Ok(SecoesFilial);
        }

        [HttpPost("/secoesfilial")]
        public async Task<ActionResult<SecoesFilial>> Post([FromBody] SecoesFilial SecoesFilial)
        {
            if (SecoesFilial == null)
            {
                return BadRequest(new { message = "Seção Filial não pode ser nula" });
            }
            _context.SecoesFilial.Add(SecoesFilial);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = SecoesFilial.id_secao_filial }, SecoesFilial);
        }

        [HttpPut("/secoesfilial/{id}")]
        public async Task<ActionResult<SecoesFilial>> Put(int id, [FromBody] SecoesFilial SecoesFilial)
        {
            if (id != SecoesFilial.id_secao)
            {
                return BadRequest(new { message = "ID da seção filial incorreto!" });
            }
            _context.Entry(SecoesFilial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/secoesfilial/{id}")]
        public async Task<ActionResult<SecoesFilial>> Delete(int id)
        {
            var SecoesFilial = await _context.SecoesFilial.FindAsync(id);
            if (SecoesFilial == null)
            {
                return NotFound(new { message = "Seção Filial não encontrada" });
            }
            _context.SecoesFilial.Remove(SecoesFilial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
