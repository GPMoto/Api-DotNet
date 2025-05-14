using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilialController : ControllerBase
    {

        private readonly AppDbContext _context;

        public FilialController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/filiais")]
        public async Task<ActionResult<IEnumerable<Filial>>> Get()
        {
            var filiais = await _context.Filial.ToListAsync();
            if (filiais == null)
            {
                return NotFound(new { message = "Filiais não encontrada" });
            }
            return Ok(filiais);
        }

        [HttpGet("/filiais/{id}")]
        public async Task<ActionResult<Filial>> GetById(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return Ok(filial);
        }

        [HttpGet("/filiais/cnpj/{cnpj}")]
        public async Task<ActionResult<Filial>> GetByCnph(string cnpj)
        {
            var filial = await _context.Filial.Where(f => f.Cnpj == cnpj).ToListAsync();
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            return Ok(filial);
        }

        [HttpPost("/filiais")]
        public async Task<ActionResult<Filial>> Post([FromBody] Filial filial)
        {
            if (filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            _context.Filial.Add(filial);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = filial.id_filial }, filial);
        }

        [HttpPut("/filiais/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Filial filial)
        {
            if(id != filial.id_filial)
            {
                return NotFound(new { StatusCode=400, message = "Id da filial não está correto" });
            }
            _context.Entry(filial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/filiais/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            if(filial == null)
            {
                return NotFound(new { message = "Filial não encontrada" });
            }
            _context.Filial.Remove(filial);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
