using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CidadeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("/cidades")]
        public async Task<ActionResult<IEnumerable<Cidade>>> Get()
        {
            return await _context.Cidade.ToListAsync();
        }


        [HttpGet("/cidades/{id}")]
        public async Task<ActionResult<Cidade>> GetById(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound(new { message = "Cidade não encontrada" });
            }
            return Ok(cidade);
        }


        [HttpPost("/cidades")]
        public async Task<ActionResult<Cidade>> Post([FromBody] Cidade cidade)
        {
            if (cidade == null)
            {
                return BadRequest(new { message = "Cidade não pode ser nula" });
            }
            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cidade.id_cidade }, cidade);
        }

        [HttpPut("/cidades/{id}")]
        public async Task<ActionResult<Cidade>> Put(int id, [FromBody] Cidade cidade)
        {
            if (id != cidade.id_cidade)
            {
                return BadRequest(new { message = "Id da cidade incorreto!" });
            }
            _context.Entry(cidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/cidades/{id}")]
        public async Task<ActionResult<Cidade>> Delete(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound(new { message = "Cidade não encontrada" });
            }
            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
