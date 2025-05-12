using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/enderecos")]
        public async Task<ActionResult<IEnumerable<Endereco>>> Get()
        {
            return await _context.Endereco.ToListAsync();
        }
        
        
        [HttpGet("/enderecos/{id}")]
        public async Task<ActionResult<Endereco>> GetById(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            return Ok(endereco);
        }

        [HttpGet("/enderecos/cep/{cep}")]
        public async Task<ActionResult<Endereco>> GetByCep(string cep)
        {
            var endereco = await _context.Endereco.Where(e => e.Cep == cep).ToListAsync();
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            return Ok(endereco);
        }

        [HttpGet("/enderecos/filial/{id}")]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetByIdFilial(int id)
        {
            var endereco = await _context.Endereco.Where(e => e.id_filial == id).ToListAsync();
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            return Ok(endereco);
        }

        [HttpPost("/enderecos")]
        public async Task<ActionResult<Endereco>> Post([FromBody] Endereco endereco)
        {
            if (endereco == null)
            {
                return BadRequest(new { message = "Endereco não pode ser nulo" });
            }
            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = endereco.id_endereco }, endereco);
        }

        [HttpPut("/enderecos/{id}")]
        public async Task<ActionResult<Endereco>> Put(int id, [FromBody] Endereco endereco)
        {
            if (id != endereco.id_endereco)
            {
                return BadRequest(new { message = "Id do endereco incorreto!" });
            }
            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/enderecos/{id}")]
        public async Task<ActionResult<Endereco>> Delete(int id)
        {
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereco não encontrado" });
            }
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
