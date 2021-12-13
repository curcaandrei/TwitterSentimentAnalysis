﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
namespace Domain
{
    public partial class TweetML
    {
        /// <summary>
        /// model input class for TweetML.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"Label")]
            public float Label { get; set; }

            [ColumnName(@"Text")]
            public string Text { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for TweetML.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public float Prediction { get; set; }

            public float[] Score { get; set; }
        }

        #endregion
        private static string GetPath()
        {
            var path = "";
            path = path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                          "/../../ProiectDotNet/Backend/Domain/TweetML.zip";
            return path;
        }

        private static string MLNetModelPath = GetPath();

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            Microsoft.ML.PredictionEngine<Domain.TweetML.ModelInput, Domain.TweetML.ModelOutput> predEngine;
            try
            { 
                predEngine = PredictEngine.Value;
            }
            catch (System.AggregateException)
            {
                MLNetModelPath = "Backend/Domain/TweetML.zip";
                predEngine = PredictEngine.Value;
            }
            
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
