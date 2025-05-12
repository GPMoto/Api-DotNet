using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UwbController : ControllerBase
    {

        private readonly AppDbContext _context;

        public UwbController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/uwb")]
        public async Task<ActionResult<IEnumerable<Uwb>>> Get()
        {
            return await _context.Uwb.ToListAsync();
        }

        [HttpGet("/uwb/{id}")]
        public async Task<ActionResult<Uwb>> GetById(int id)
        {
            var uwb = await _context.Uwb.FindAsync(id);
            if (uwb == null)
            {
                return NotFound(new { message = "UWB não encontrado" });
            }
            return Ok(uwb);
        }

        [HttpPost("/uwb")]
        public async Task<ActionResult<Uwb>> Post([FromBody] Uwb uwb)
        {
            if (uwb == null)
            {
                return BadRequest(new { message = "UWB não pode ser nulo" });
            }
            _context.Uwb.Add(uwb);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = uwb.id_uwb }, uwb);
        }

        [HttpPut("/uwb/{id}")]
        public async Task<ActionResult<Uwb>> Put(int id, [FromBody] Uwb uwb)
        {
            if (id != uwb.id_uwb)
            {
                return BadRequest(new { message = "Id do UWB incorreto!" });
            }
            _context.Entry(uwb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/uwb/{id}")]
        public async Task<ActionResult<Uwb>> Delete(int id)
        {
            var uwb = await _context.Uwb.FindAsync(id);
            if (uwb == null)
            {
                return NotFound(new { message = "UWB não encontrado" });
            }
            _context.Uwb.Remove(uwb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
