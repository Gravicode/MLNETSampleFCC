using Microsoft.ML;
using MLNetSampleFCCML.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCApp.Helpers
{
    public class Dataset
    {
        public static string DATA_FILEPATH = GetAbsolutePath(@"../../../Dataset\LasVegasTripAdvisorReviews-Dataset.csv");
        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public static (DataViewSchema schema, IEnumerable<ModelInput> data) GetSampleData(string dataFilePath,int Count=10)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ';',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            var sampleForPredictions = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .Take(Count);
            return (dataView.Schema, sampleForPredictions);
        }
    }
    public class Predictor
    {
        private readonly string _modelfile;
        private static MLContext context;
        private static ITransformer model;
        private static PredictionEngine<ModelInput, ModelOutputWithContribution> predictionEngine;
        public Predictor(string modelfile)
        {
            _modelfile = modelfile ?? throw new ArgumentNullException(nameof(modelfile));

            context = new MLContext();

            model = context.Model.Load(_modelfile, out var inputSchema);

            predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutputWithContribution>(model);
        }

        public List<ScorePrediction> RunMultiplePredictions(int numberOfPredictions)
        {
            Console.WriteLine($"Predictions from saved model:");

            Console.WriteLine($"\n \n Test {numberOfPredictions} transactions, from the test datasource");

            List<ScorePrediction> transactionList = new List<ScorePrediction>();
            ModelOutputWithContribution prediction;
            ScorePrediction explainedPrediction;

            var datas = Dataset.GetSampleData(Dataset.DATA_FILEPATH, numberOfPredictions);

                        datas.data.ToList()
                        .ForEach(testData =>
                        {
                            testData.PrintToConsole();
                            prediction = predictionEngine.Predict(testData);
                            explainedPrediction = new ScorePrediction(prediction.Score, prediction.GetFeatureContributions(model.GetOutputSchema(datas.schema)));
                            transactionList.Add(explainedPrediction);
                        });

            return transactionList;
        }

    }
}
