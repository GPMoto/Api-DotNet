using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Exceptions;
using WebApplication3.Models;
using WebApplication3.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService usuarioService;

        public UsuarioController(UsuarioService context)
        {
            usuarioService = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os usuários.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /usuarios
        ///
        /// </remarks>
        /// <returns>Uma lista de usuários</returns>
        /// <response code="200">Retorna a lista completa de usuários</response>
        [HttpGet("/usuarios")]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), 200)]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return Ok(await usuarioService.GetAllAsync());

        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /usuarios/1
        ///
        /// </remarks>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Retorna o usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("/usuarios/{id}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario não encontrado" });
            }
            return Ok(usuario);

        }

        /// <summary>
        /// Obtém usuários por ID da filial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /usuarios/filial/1
        ///
        /// </remarks>
        /// <param name="id">ID da filial</param>
        /// <returns>Lista de usuários da filial</returns>
        /// <response code="200">Retorna os usuários da filial</response>
        /// <response code="404">Usuários não encontrados</response>
        [HttpGet("/usuarios/filial/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetByIdFilial(int id)
        {
            var usuarios = await usuarioService.getByIdFilial(id);
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { message = "Usuários não encontrados" });
            }
            return Ok(usuarios);

        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /usuarios
        ///     {
        ///         "id_usuario": 1,
        ///         "NomeUsuario": "João Silva",
        ///         "EmailUsuario": "joao@email.com",
        ///         "SenhaUsuario": "senha123",
        ///         "id_perfil": 2,
        ///         "id_filial": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Usuário criado</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("/usuarios")]
        [ProducesResponseType(typeof(Usuario), 201)]
        [ProducesResponseType(400)]
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
                var created = await usuarioService.AddAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = created.id_usuario }, created);

            }
            catch (EmailInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /usuarios/1
        ///     {
        ///         "id_usuario": 1,
        ///         "NomeUsuario": "João Silva",
        ///         "EmailUsuario": "joao@email.com",
        ///         "SenhaUsuario": "novaSenha",
        ///         "id_perfil": 2,
        ///         "id_filial": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do usuário a ser atualizado</param>
        /// <param name="usuario">Dados atualizados do usuário</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Usuário atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID incorreto</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut("/usuarios/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
                if (id != usuario.id_usuario)
                {
                    return BadRequest(new { StatusCode = 400, message = "Id do usuario incorreto!" });
                }
                var existingUsuario = await usuarioService.GetByIdAsync(id);
                if (existingUsuario == null)
                {
                    return NotFound(new { message = "Usuario não encontrado" });
                }
                await usuarioService.UpdateAsync(usuario);
                return NoContent();

            }
            catch (EmailInvalidoException error)
            {
                return BadRequest(new { StatusCode = 400, message = error.Message });
            }
        }

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /usuarios/1
        ///
        /// </remarks>
        /// <param name="id">ID do usuário a ser removido</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        /// <response code="204">Usuário removido com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("/usuarios/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var existingUsuario = await usuarioService.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound(new { message = "Usuario não encontrado" });
            }
            var result = await usuarioService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Usuario não encontrado" });
            }
            return NoContent();

        }

    }
}
