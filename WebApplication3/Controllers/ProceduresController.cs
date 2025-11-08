using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProceduresController : ControllerBase
    {
        private readonly OracleFunctionsService _functionsService;
        private readonly UsuarioService _usuarioService;

        public ProceduresController(OracleFunctionsService functionsService, UsuarioService usuarioService)
        {
            _functionsService = functionsService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtém todos os usuários processados de forma simples
        /// </summary>
        /// <returns>Lista de usuários com dados básicos</returns>
        [HttpGet("usuarios-processados")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetUsuariosProcessados()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                var usuariosProcessados = new List<object>();

                foreach (var usuario in usuarios)
                {
                    // Valida o email usando a função Oracle
                    int emailValido = _functionsService.ValidaEmailUser(usuario.EmailUsuario);

                    var usuarioProcessado = new
                    {
                        id = usuario.id_usuario,
                        nome = usuario.NomeUsuario,
                        email = usuario.EmailUsuario,
                        emailValido = emailValido == 1,
                        idFilial = usuario.id_filial
                    };

                    usuariosProcessados.Add(usuarioProcessado);
                }

                return Ok(new
                {
                    total = usuariosProcessados.Count,
                    usuarios = usuariosProcessados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao processar usuários: {ex.Message}" });
            }
        }

        /// <summary>
        /// Gera um JSON formatado com chave e valor
        /// </summary>
        /// <param name="chave">A chave do JSON</param>
        /// <param name="valor">O valor do JSON</param>
        /// <returns>String JSON formatada</returns>
        [HttpGet("printa-json")]
        [AllowAnonymous]
        public ActionResult<object> PrintaJson(
            [FromQuery(Name = "chave")] string chave,
            [FromQuery(Name = "valor")] string valor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(chave) || string.IsNullOrWhiteSpace(valor))
                {
                    return BadRequest(new { erro = "Chave e valor são obrigatórios" });
                }

                string resultado = _functionsService.PrintaJson(chave, valor);

                // Tenta converter para objeto JSON
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(resultado);
                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao processar: {ex.Message}" });
            }
        }

        /// <summary>
        /// Valida um email de usuário
        /// </summary>
        /// <param name="email">O email a ser validado</param>
        /// <returns>1 se válido, 0 se inválido</returns>
        [HttpGet("valida-email")]
        public ActionResult<object> ValidaEmail([FromQuery(Name = "email")] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest(new { erro = "Email é obrigatório" });
                }

                int resultado = _functionsService.ValidaEmailUser(email);

                return Ok(new
                {
                    email = email,
                    valido = resultado == 1,
                    codigo = resultado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao validar email: {ex.Message}" });
            }
        }

        /// <summary>
        /// Gera múltiplos JSONs em um objeto
        /// </summary>
        [HttpPost("gera-jsons")]
        public ActionResult<object> GeraMultiplosJsons([FromBody] List<JsonRequest> dados)
        {
            try
            {
                if (dados == null || dados.Count == 0)
                {
                    return BadRequest(new { erro = "Lista de dados é obrigatória" });
                }

                var resultados = new List<object>();

                foreach (var item in dados)
                {
                    string json = _functionsService.PrintaJson(item.Chave, item.Valor);
                    var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(json);
                    resultados.Add(jsonObject);
                }

                return Ok(new { total = resultados.Count, dados = resultados });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao gerar JSONs: {ex.Message}" });
            }
        }

        /// <summary>
        /// Testa a conexão com o banco de dados
        /// </summary>
        [HttpGet("health")]
        public ActionResult<object> Health()
        {
            try
            {
                // Testa chamar uma função simples para testar conexão
                string teste = _functionsService.PrintaJson("status", "ok");

                if (teste.Contains("erro"))
                {
                    return StatusCode(503, new { status = "indisponível", mensagem = "Banco de dados indisponível" });
                }

                return Ok(new { status = "disponível", mensagem = "Conexão com banco de dados OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new { status = "indisponível", mensagem = ex.Message });
            }
        }
    }

    /// <summary>
    /// DTO para requisição de múltiplos JSONs
    /// </summary>
    public class JsonRequest
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}