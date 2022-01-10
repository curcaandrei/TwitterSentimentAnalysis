using System;
using System.Collections.Generic;
using Application.Persistence;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.ML;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<List<TweetDto>> ListAllAsync()
        {
            List<Tweet> tweets =  _context.GetCollection<Tweet>("RequestedTweets").Find(_ => true).ToList();
            List<TweetDto> tweetDtos = new List<TweetDto>();
            
            foreach (var variable in tweets)
            {
                Tweet t = variable;

                TweetDto tweetDto = new TweetDto();
                if (t != null)
                {
                    tweetDto.User = t.User;
                    tweetDto.Username = t.Username;
                    tweetDto.Date = t.Date;
                    tweetDto.Id = t.Id.ToString();
                    tweetDto.Text = t.Text;
                    tweetDto.Feels = t.feels;
                    tweetDtos.Add(tweetDto);
                }
            }

            return await Task.FromResult(tweetDtos);
        }

        public async Task<Tweet> AddAsync(Tweet entity)
        {
            await _context.GetCollection<Tweet>("RequestedTweets").InsertOneAsync(entity);
            return entity;
        }

        public DeleteResult DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<TweetDto>("RequestedTweets").DeleteOne(Builders<TweetDto>.Filter.Eq("_id", objectId));
        }

        public void RetrainAlgorithm(string id)
        {
            var objectId = new ObjectId(id);
            var modelInput = _collection.Find(x => x.Id == objectId).ToList();
            var result = modelInput[0].feels["sad"] > modelInput[0].feels["happy"] ? 0 : 4;
            
            var mlContext = new MLContext();

            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"..\\Domain\\shuffled_tweets.csv")), true))
            {
                csvTable.Load(csvReader);
            }
            List<TweetML.ModelInput> dataList = new List<TweetML.ModelInput>();

            for(int i = 0; i < csvTable.Rows.Count; i++)
            {
                var row = csvTable.Rows[i];
                dataList.Add(new TweetML.ModelInput() { Label = Convert.ToSingle(row.ItemArray[0]), Text = Convert.ToString(row.ItemArray[1]) ?? string.Empty });
            }
            dataList.Add(new TweetML.ModelInput()
            {
                Text = modelInput[0].Text,
                Label = result
            });
            TweetML.ModelInput[] validData = dataList.ToArray();

           IDataView newData = mlContext.Data.LoadFromEnumerable<TweetML.ModelInput>(validData);
            var pipelineEstimator =
            mlContext.Transforms.Text.FeaturizeText(@"Text", @"Text")
                                    .Append(mlContext.Transforms.Concatenate(@"Features", @"Text"))
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(@"Label", @"Label"))
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(l1Regularization: 3.26121927060575F, l2Regularization: 59.2224549814234F, labelColumnName: @"Label", featureColumnName: @"Features"), labelColumnName: @"Label"))
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(@"PredictedLabel", @"PredictedLabel"));
            ITransformer trainedModel = pipelineEstimator.Fit(newData);
            mlContext.Model.Save(trainedModel, newData.Schema, "..\\Domain\\TweetML.zip");

            var res = _context.GetCollection<TweetDto>("RequestedTweets").DeleteOne(Builders<TweetDto>.Filter.Eq("_id", objectId));
            Console.WriteLine(res.DeletedCount);
        }
    }
}