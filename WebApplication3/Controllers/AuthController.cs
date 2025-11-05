using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTO;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {

        private readonly TokenService tokenService;
        private readonly UsuarioService _usuarioService;

        public AuthController(TokenService tokenService, UsuarioService userService)
        {
            this.tokenService = tokenService;
            _usuarioService = userService;

        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult GenerateToken([FromBody] UserDTO login)
        {
            try
            {
                var usuario = _usuarioService.getByEmail(login.Email);
                if (usuario == null || usuario.Result.SenhaUsuario != login.Password)
                {
                    return new UnauthorizedResult();
                }
                var token = tokenService.GenerateToken(usuario.Result.EmailUsuario);
                return new OkObjectResult(new { token = token });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = "Erro ao gerar token", details = ex.Message });
            }
        }
    }
}
