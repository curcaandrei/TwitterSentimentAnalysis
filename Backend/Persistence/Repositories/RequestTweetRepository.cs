using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Persistence;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.Repositories
{
    public class RequestTweetRepository : IRequestTweetRepository
    {
        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Tweet> _collection;
        
        public RequestTweetRepository(IMongoDbContext dbContext)
        {
            _context = dbContext;
            _collection = dbContext.GetCollection<Tweet>("RequestedTweets");
        }
        
        public async Task<TweetDto> AddAsync(TweetDto entity)
        {
            await _context.GetCollection<TweetDto>("RequestedTweets").InsertOneAsync(entity);
            return entity;
        }

        public DeleteResult DeleteAsync(string id)
        {
            var objectId = id;
            return _context.GetCollection<TweetDto>("RequestedTweets").DeleteOne(Builders<TweetDto>.Filter.Eq("_id", objectId));
        }

        public void RetrainAlgorithm(string id)
        {
            Console.WriteLine("COCK1");
            //var modelInput = _context.GetCollection<TweetDto>("RequestedTweets").FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
            TweetML.ModelInput modelInput = new TweetML.ModelInput();
            modelInput.Label = 1;
            modelInput.Text = "string";
            Console.WriteLine("COCK2");
            //var result = modelInput.Result.Feels["sad"] > modelInput.Result.Feels["happy"] ? 0 : 1;
            
            var mlContext = new MLContext();
            Console.WriteLine("COCK3");

            DataViewSchema dataPrepPipelineSchema, modelSchema;
            ITransformer dataPrepPipeline = mlContext.Model.Load("C:\\Users\\Andrei\\Documents\\GitHub\\ProiectDotNet\\Backend\\Domain\\TweetML.zip", out dataPrepPipelineSchema);
            Console.WriteLine("COCK4");

            ITransformer trainedModel = mlContext.Model.Load("C:\\Users\\Andrei\\Documents\\GitHub\\ProiectDotNet\\Backend\\Domain\\TweetML.zip", out modelSchema);

            PoissonRegressionModelParameters originalModelParameters =
                ((ISingleFeaturePredictionTransformer<object>)trainedModel).Model as PoissonRegressionModelParameters;

            TweetML.ModelInput[] data = new[]
            {
                new TweetML.ModelInput()
                {
                    Text = modelInput.Text,
                    Label = modelInput.Label
                }
            };
            IDataView newData = mlContext.Data.LoadFromEnumerable<TweetML.ModelInput>(data);
            Console.WriteLine("COCK6");

            IDataView transformedNewData = dataPrepPipeline.Transform(newData);

            RegressionPredictionTransformer<PoissonRegressionModelParameters> retrainedModel =
                mlContext.Regression.Trainers.LbfgsPoissonRegression()
                    .Fit(transformedNewData, originalModelParameters);

            PoissonRegressionModelParameters retrainedModelParameters = retrainedModel.Model as PoissonRegressionModelParameters;

            var weightDiffs =
                originalModelParameters.Weights.Zip(
                    retrainedModelParameters.Weights, (original, retrained) => original - retrained).ToArray();
            Console.WriteLine("Original | Retrained | Difference");
            for(int i=0;i < weightDiffs.Count();i++)
            {
                Console.WriteLine($"{originalModelParameters.Weights[i]} | {retrainedModelParameters.Weights[i]} | {weightDiffs[i]}");
            }
            DeleteAsync(id);
        }
    }
}