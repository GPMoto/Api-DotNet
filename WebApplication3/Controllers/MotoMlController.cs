using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTO;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{

    /// <summary>
    /// Controller para predições ML de condição de motos
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class MotoMlController : ControllerBase
    {

        private readonly MotoMlService _motoMLService;

        public MotoMlController(MotoMlService motoMLService)
        {
            _motoMLService = motoMLService;
        }

        /// <summary>
        /// Prediz a condição de uma moto usando ML
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///     POST /motoml/predict
        ///     {
        ///         "diasUso": 45,
        ///         "status": 1,
        ///         "tipoMoto": "Urbana"
        ///     }
        /// </remarks>
        [HttpPost("/motoml/predict")]
        [ProducesResponseType(typeof(MotoConditionResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(503)]
        public async Task<ActionResult<MotoConditionResponse>> PredictCondition([FromBody] MotoConditionRequest request)
        {
            try
            {
                // Validações básicas
                if (request.DiasUso < 0)
                    return BadRequest(new { message = "DiasUso não pode ser negativo" });

                if (request.Status != 0 && request.Status != 1)
                    return BadRequest(new { message = "Status deve ser 0 ou 1" });

                var tiposValidos = new[] { "Esportiva", "Urbana", "Scooter" };
                if (!tiposValidos.Contains(request.TipoMoto))
                    return BadRequest(new { message = $"TipoMoto deve ser: {string.Join(", ", tiposValidos)}" });

                // Verificar modelo
                if (!_motoMLService.IsModelAvailable())
                    return StatusCode(503, new { message = "Modelo ML não disponível. Execute o MLTrainer primeiro." });

                // Fazer predição
                var response = await _motoMLService.PredictConditionAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", details = ex.Message });
            }
        }

        /// <summary>
        /// Verifica status do modelo ML
        /// </summary>
        [HttpGet("/motoml/status")]
        public IActionResult GetModelStatus()
        {
            var isAvailable = _motoMLService.IsModelAvailable();

            return Ok(new
            {
                ModelAvailable = isAvailable,
                Status = isAvailable ? "✅ Disponível" : "❌ Não encontrado",
                Message = isAvailable ? "Pronto para predições" : "Execute MLTrainer primeiro",
                CheckedAt = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Teste rápido do modelo
        /// </summary>
        [HttpGet("/motoml/test")]
        public async Task<ActionResult<MotoConditionResponse>> TestModel()
        {
            if (!_motoMLService.IsModelAvailable())
                return StatusCode(503, new { message = "Modelo não disponível" });

            var testRequest = new MotoConditionRequest
            {
                DiasUso = 45f,
                Status = 1f,
                TipoMoto = "Urbana"
            };

            try
            {
                var response = await _motoMLService.PredictConditionAsync(testRequest);
                return Ok(new
                {
                    TestStatus = "✅ Sucesso",
                    TestData = testRequest,
                    Prediction = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro no teste", details = ex.Message });
            }
        }
    }
}
