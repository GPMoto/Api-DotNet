using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TipoSecaoFilialController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoSecaoFilialController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/tiposecaofilial")]
        public async Task<ActionResult<IEnumerable<TipoSecao>>> Get()
        {
            return await _context.TipoSecao.ToListAsync();
        }

        [HttpGet("/tiposecaofilial/{id}")]
        public async Task<ActionResult<TipoSecao>> GetById(int id)
        {
            var tipoSecaoFilial = await _context.TipoSecao.FindAsync(id);
            if (tipoSecaoFilial == null)
            {
                return NotFound(new { message = "Tipo de seção filial não encontrado" });
            }
            return Ok(tipoSecaoFilial);
        }

        [HttpPost("/tiposecaofilial")]
        public async Task<ActionResult<TipoSecao>> Post([FromBody] TipoSecao tipoSecaoFilial)
        {
            if (tipoSecaoFilial == null)
            {
                return BadRequest(new { message = "Tipo de seção filial não pode ser nulo" });
            }
            _context.TipoSecao.Add(tipoSecaoFilial);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = tipoSecaoFilial.id_tipo_secao }, tipoSecaoFilial);
        }

        [HttpPut("/tiposecaofilial/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoSecao tipoSecaoFilial)
        {
            if (id != tipoSecaoFilial.id_tipo_secao)
            {
                return BadRequest(new {StatusCode=400, message = "Id do tipo de seção filial incorreto!" });
            }
            _context.Entry(tipoSecaoFilial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/tiposecaofilial/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoSecaoFilial = await _context.TipoSecao.FindAsync(id);
            if (tipoSecaoFilial == null)
            {
                return NotFound(new { message = "Tipo de seção filial não encontrado" });
            }
            _context.TipoSecao.Remove(tipoSecaoFilial);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
