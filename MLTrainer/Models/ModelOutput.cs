using Microsoft.ML.Data;

namespace MLTrainer.Models
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string CondicaoMoto { get; set; } = string.Empty; // "Nova", "Boa", "Regular", "Ruim"

        [ColumnName("Score")]
        public float[] Score { get; set; } = [];
    }
}