namespace WebApplication3.DTO
{
    public class MotoConditionResponse
    {
        public string CondicaoMoto { get; set; } = string.Empty;

        public float Confianca { get; set; }

        public string Recomendacao { get; set; } = string.Empty;

        public string NivelPrioridade { get; set; } = string.Empty;

        public MotoConditionRequest DadosEntrada { get; set; } = new();

        public DateTime DataPredicao { get; set; } = DateTime.UtcNow;

    }
}
