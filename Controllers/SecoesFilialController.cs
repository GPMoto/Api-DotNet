using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
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
            try
            {
                if (SecoesFilial == null)
                {
                    return BadRequest(new { message = "Seção Filial não pode ser nula" });
                }
                if (SecoesFilial.Lado4 >10000 || SecoesFilial.Lado4 <= 0)
                {
                    throw new TamanhoInvalidoException(10000,1,"lado");
                }
                if (SecoesFilial.Lado1 > 10000 || SecoesFilial.Lado1 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado2 > 10000 || SecoesFilial.Lado2 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                if (SecoesFilial.Lado3 > 10000 || SecoesFilial.Lado3 <= 0)
                {
                    throw new TamanhoInvalidoException(10000, 1, "lado");
                }
                _context.SecoesFilial.Add(SecoesFilial);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = SecoesFilial.id_secao }, SecoesFilial);
            }catch(TamanhoInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        [HttpPut("/secoesfilial/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SecoesFilial SecoesFilial)
        {
            if (id != SecoesFilial.id_secao)
            {
                return BadRequest(new {StatusCode=400, message = "Id da seção filial incorreto!" });
            }
            _context.Entry(SecoesFilial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/secoesfilial/{id}")]
        public async Task<ActionResult> Delete(int id)
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
