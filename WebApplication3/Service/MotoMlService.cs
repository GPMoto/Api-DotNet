using Microsoft.ML;
using MLTrainer.Models;
using WebApplication3.DTO;

namespace WebApplication3.Service
{
    public class MotoMlService
    {

        private readonly MLContext _mlContext;
        private readonly string _modelPath;
        private ITransformer? _model;

        public MotoMlService()
        {
            _mlContext = new MLContext(seed: 0);
            _modelPath = Path.Combine(Environment.CurrentDirectory, "moto-condition-model.zip");
        }

        public async Task<MotoConditionResponse> PredictConditionAsync(MotoConditionRequest request)
        {
            try
            {
                await EnsureModelLoadedAsync();

                var input = new ModelInput
                {
                    DiasUso = request.DiasUso,
                    Status = request.Status,
                    TipoMoto = request.TipoMoto,
                    Label = string.Empty
                };

                var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model!);
                var prediction = predictionEngine.Predict(input);

                var confianca = CalculateConfidence(prediction.Score);

                var response = new MotoConditionResponse
                {
                    CondicaoMoto = prediction.CondicaoMoto,
                    Confianca = confianca,
                    Recomendacao = GetRecommendation(prediction.CondicaoMoto),
                    NivelPrioridade = GetPriorityLevel(prediction.CondicaoMoto),
                    DadosEntrada = request,
                    DataPredicao = DateTime.UtcNow
                };

                Console.WriteLine($" Predição ML: {prediction.CondicaoMoto} com confiança {confianca:P1}");

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Erro na predição ML: {ex.Message}");
                throw new InvalidOperationException($"Erro na predição ML: {ex.Message}");
            }
        }

        public bool IsModelAvailable()
        {
            return File.Exists(_modelPath);
        }


        private async Task EnsureModelLoadedAsync()
        {
            if (_model != null) return;

            if (!File.Exists(_modelPath))
            {
                throw new FileNotFoundException($"Modelo ML não encontrado em: {_modelPath}. Execute o MLTrainer primeiro.");
            }

            await Task.Run(() =>
            {
                _model = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
                Console.WriteLine(" Modelo ML carregado com sucesso");
            });
        }

        private float CalculateConfidence(float[] scores)
        {
            if (scores == null || scores.Length == 0)
            {
                Console.WriteLine(" Array de scores vazio");
                return 0f;
            }

            var maxScore = scores.Max();
            return Math.Abs(maxScore); // Garantir valor positivo
        }


        private string GetRecommendation(string condicao)
        {
            return condicao switch
            {
                "Nova" => " Moto em excelente estado - Manter manutenção preventiva",
                "Boa" => " Moto em bom estado - Acompanhar cronograma de manutenção",
                "Regular" => " Moto precisa de atenção - Agendar manutenção corretiva",
                "Ruim" => " Moto necessita intervenção urgente - Retirar de operação",
                _ => " Condição não identificada - Avaliar manualmente"
            };
        }


        private string GetPriorityLevel(string condicao)
        {
            return condicao switch
            {
                "Nova" => "Muito Baixa",
                "Boa" => "Baixa",
                "Regular" => "Média",
                "Ruim" => "Alta",
                _ => "Indefinida"
            };
        }

        public async Task<object> GetModelInfoAsync()
        {
            await EnsureModelLoadedAsync();

            return new
            {
                ModelPath = _modelPath,
                ModelLoaded = _model != null,
                InputFeatures = new[] { "DiasUso", "Status", "TipoMoto" },
                OutputLabel = "CondicaoMoto",
                PossibleConditions = new[] { "Nova", "Boa", "Regular", "Ruim" },
                LoadedAt = DateTime.UtcNow
            };

        }
    }
}
