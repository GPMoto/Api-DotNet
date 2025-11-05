using Microsoft.ML.Data;

namespace MLTrainer.Models
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public float DiasUso { get; set; }

        [LoadColumn(1)]
        public float Status { get; set; }

        [LoadColumn(2)]
        public string TipoMoto { get; set; } = string.Empty;

        [LoadColumn(3), ColumnName("Label")]
        public string Label { get; set; } = string.Empty;
    }
}