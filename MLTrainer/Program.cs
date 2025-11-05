using Microsoft.ML;
using MLTrainer.Models;


var mlContext = new MLContext();

var dadosTreino = new List<ModelInput>()
{
    new() { DiasUso = 5, Status = 1, TipoMoto = "Esportiva", Label = "Nova" },
    new() { DiasUso = 15, Status = 1, TipoMoto = "Urbana", Label = "Nova" },
    new() { DiasUso = 25, Status = 1, TipoMoto = "Scooter", Label = "Nova" },
    
    new() { DiasUso = 45, Status = 1, TipoMoto = "Esportiva", Label = "Boa" },
    new() { DiasUso = 60, Status = 1, TipoMoto = "Urbana", Label = "Boa" },
    new() { DiasUso = 85, Status = 1, TipoMoto = "Scooter", Label = "Boa" },
    
    new() { DiasUso = 120, Status = 1, TipoMoto = "Esportiva", Label = "Regular" },
    new() { DiasUso = 150, Status = 1, TipoMoto = "Urbana", Label = "Regular" },
    new() { DiasUso = 180, Status = 1, TipoMoto = "Scooter", Label = "Regular" },
    
    new() { DiasUso = 250, Status = 1, TipoMoto = "Esportiva", Label = "Ruim" },
    new() { DiasUso = 300, Status = 0, TipoMoto = "Urbana", Label = "Ruim" },
    new() { DiasUso = 350, Status = 0, TipoMoto = "Scooter", Label = "Ruim" },
    
    new() { DiasUso = 10, Status = 1, TipoMoto = "Esportiva", Label = "Nova" },
    new() { DiasUso = 70, Status = 1, TipoMoto = "Urbana", Label = "Boa" },
    new() { DiasUso = 140, Status = 1, TipoMoto = "Scooter", Label = "Regular" },
    new() { DiasUso = 280, Status = 0, TipoMoto = "Esportiva", Label = "Ruim" }
};

Console.WriteLine($" Criados {dadosTreino.Count} exemplos de treinamento");

var dataView = mlContext.Data.LoadFromEnumerable(dadosTreino);

var pipeline = mlContext.Transforms.CopyColumns("Label", "Label")
    .Append(mlContext.Transforms.Text.FeaturizeText("TipoMotoFeatures", "TipoMoto"))
    .Append(mlContext.Transforms.NormalizeMinMax("DiasUsoNorm", "DiasUso"))
    .Append(mlContext.Transforms.NormalizeMinMax("StatusNorm", "Status"))
    .Append(mlContext.Transforms.Concatenate("Features", "TipoMotoFeatures", "DiasUsoNorm", "StatusNorm"))
    .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"))
    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


Console.WriteLine(" Treinando modelo...");

var modelo = pipeline.Fit(dataView);

Console.WriteLine(" Modelo treinado com sucesso!");

var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(modelo);

Console.WriteLine("\n=== Testando Predições ===");

var modelPath = Path.Combine(Environment.CurrentDirectory, "moto-condition-model.zip");
mlContext.Model.Save(modelo, dataView.Schema, modelPath);

Console.WriteLine($" Modelo salvo em: {modelPath}");

