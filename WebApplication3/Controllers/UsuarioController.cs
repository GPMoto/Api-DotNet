using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await _context.Usuario.ToListAsync();
        }

        [HttpGet("/usuarios/{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuario não encontrado" });
                }
                return Ok(usuario);
        }

        [HttpGet("/usuarios/filial/{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetByIdFilial(int id)
        {
            var usuario = await _context.Usuario.Where(u => u.id_usuario == id).ToListAsync();
            if (usuario == null)
            {
                return NotFound(new { message = "Usuarios não encontrado" });
            }
            return Ok(usuario);
        }

        [HttpPost("/usuarios")]
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new { message = "Usuario não pode ser nulo" });
                }
                if (!usuario.EmailUsuario.Contains("@"))
                {
                    throw new EmailInvalidoException();
                }
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = usuario.id_usuario }, usuario);
            }
            catch (EmailInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        [HttpPut("/usuarios/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.id_usuario)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do usuario incorreto!" });
                }
                if (!usuario.EmailUsuario.Contains("@"))
                {
                    throw new EmailInvalidoException();
                }
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (EmailInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        [HttpDelete("/usuarios/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario não encontrado" });
            }
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
