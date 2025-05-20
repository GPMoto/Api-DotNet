using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {


        private readonly AppDbContext _context;

        public ContatoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/contatos")]
        public async Task<ActionResult<IEnumerable<Contato>>> Get()
        {
            return await _context.Contato.ToListAsync();
        }

        [HttpGet("/contatos/{id}")]
        public async Task<ActionResult<Contato>> GetById(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            return Ok(contato);
        }

        [HttpGet("/contatos/nomeDono/{nome}")]
        public async Task<ActionResult<Contato>> GetByNameDono(string nome)
        {
            var contato = await _context.Contato.Where(c => c.nmDono.Contains(nome)).ToListAsync();
            if(contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            return Ok(contato);
        }

        [HttpPost("/contatos")]
        public async Task<ActionResult<Contato>> Post([FromBody] Contato contato)
        {
            try
            {
                if (contato == null)
                {
                    return BadRequest(new { message = "Contato não pode ser nulo" });
                }
                if (contato.status != 0 || contato.status != 1)
                {
                    throw new StatusInvalidoException();
                }
                _context.Contato.Add(contato);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = contato.id_contato }, contato);
            }catch(StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        [HttpPut("/contatos/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Contato contato)
        {
            try
            {
                if (id != contato.id_contato)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do contato incorreto!" });
                }
                if (contato.status != 0 || contato.status != 1)
                {
                    throw new StatusInvalidoException();
                }
                _context.Entry(contato).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (StatusInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, Message = error.Message });
            }
        }

        [HttpDelete("/contatos/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }
            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
