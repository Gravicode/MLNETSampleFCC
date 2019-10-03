// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using MLNetSampleFCCML.Model;

namespace MLNetSampleFCCML.ConsoleApp
{
    public static class ModelBuilder
    {
        private static string TRAIN_DATA_FILEPATH = GetAbsolutePath("../../../../Dataset/LasVegasTripAdvisorReviews-Dataset.csv");// @"C:\experiment\mlnet\MLNetSampleFCC\Dataset\LasVegasTripAdvisorReviews-Dataset.csv";
        private static string MODEL_FILEPATH = @"../../../../MLNetSampleFCCML.Model/MLModel.zip";

        // Create MLContext to be shared across the model creation workflow objects 
        // Set a random seed for repeatable/deterministic results across multiple trainings.
        private static MLContext mlContext = new MLContext(seed: 1);
        
        public static void CreateModel()
        {
            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ';',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Build Model
            IEstimator<ITransformer> trainingPipeline = BuildModel(mlContext,trainingDataView);

            // Evaluate quality of Model
            Evaluate(mlContext, trainingDataView, trainingPipeline);

        }

        public static IEstimator<ITransformer> BuildModel(MLContext mlContext, IDataView trainingDataView)
        {
            // Data process configuration with pipeline data transformations 
            var dataProcessPipeline = mlContext.Transforms.Conversion.ConvertType(new[] { new InputOutputColumnPair("Pool", "Pool"), new InputOutputColumnPair("Gym", "Gym"), new InputOutputColumnPair("Tennis court", "Tennis court"), new InputOutputColumnPair("Spa", "Spa"), new InputOutputColumnPair("Casino", "Casino"), new InputOutputColumnPair("Free internet", "Free internet") })
                                      .Append(mlContext.Transforms.Categorical.OneHotEncoding(new[] { new InputOutputColumnPair("User country", "User country"), new InputOutputColumnPair("Period of stay", "Period of stay"), new InputOutputColumnPair("Traveler type", "Traveler type"), new InputOutputColumnPair("Hotel name", "Hotel name"), new InputOutputColumnPair("Hotel stars", "Hotel stars"), new InputOutputColumnPair("User continent", "User continent"), new InputOutputColumnPair("Review month", "Review month"), new InputOutputColumnPair("Review weekday", "Review weekday") }))
                                      .Append(mlContext.Transforms.Concatenate("Features", new[] { "Pool", "Gym", "Tennis court", "Spa", "Casino", "Free internet", "User country", "Period of stay", "Traveler type", "Hotel name", "Hotel stars", "User continent", "Review month", "Review weekday", "Nr. reviews", "Nr. hotel reviews", "Helpful votes", "Nr. rooms", "Member years" }))
                                      .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
                                      .AppendCacheCheckpoint(mlContext);

            // Set the training algorithm 
            var trainer = mlContext.Regression.Trainers.Sdca(labelColumnName: "Score", featureColumnName: "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            //add feature contribution calc
            var trainedModel = trainingPipeline.Fit(trainingDataView);

            var fccModel = trainedModel.Append(mlContext.Transforms
                .CalculateFeatureContribution(trainedModel.LastTransformer)
                .Fit(trainingPipeline.Fit(trainingDataView).Transform(trainingDataView)));

            // Evaluate the model and show accuracy stats
            Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");

            IDataView predictions = fccModel.Transform(trainingDataView);
            var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: "Score");
            var crossValidationResults = mlContext.Regression.CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "Score");
            PrintRegressionFoldsAverageMetrics(crossValidationResults);
            Common.ConsoleHelper.PrintRegressionMetrics(trainer.ToString(), metrics);

            Console.WriteLine("=============== End of training process ===============");
            SaveModel(mlContext, fccModel, MODEL_FILEPATH, trainingDataView.Schema);
            Console.WriteLine("=============== Save Model ===============");
            return trainingPipeline;
        }


        private static void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            var crossValidationResults = mlContext.Regression.CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "Score");
            PrintRegressionFoldsAverageMetrics(crossValidationResults);
        }
        private static void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public static void PrintRegressionMetrics(RegressionMetrics metrics)
        {
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics for regression model      ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       LossFn:        {metrics.LossFunction:0.##}");
            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Absolute loss: {metrics.MeanAbsoluteError:#.##}");
            Console.WriteLine($"*       Squared loss:  {metrics.MeanSquaredError:#.##}");
            Console.WriteLine($"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        public static void PrintRegressionFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<RegressionMetrics>> crossValidationResults)
        {
            var L1 = crossValidationResults.Select(r => r.Metrics.MeanAbsoluteError);
            var L2 = crossValidationResults.Select(r => r.Metrics.MeanSquaredError);
            var RMS = crossValidationResults.Select(r => r.Metrics.RootMeanSquaredError);
            var lossFunction = crossValidationResults.Select(r => r.Metrics.LossFunction);
            var R2 = crossValidationResults.Select(r => r.Metrics.RSquared);

            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Regression model      ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       Average L1 Loss:       {L1.Average():0.###} ");
            Console.WriteLine($"*       Average L2 Loss:       {L2.Average():0.###}  ");
            Console.WriteLine($"*       Average RMS:           {RMS.Average():0.###}  ");
            Console.WriteLine($"*       Average Loss Function: {lossFunction.Average():0.###}  ");
            Console.WriteLine($"*       Average R-squared:     {R2.Average():0.###}  ");
            Console.WriteLine($"*************************************************************************************************************");
        }
    }
}
