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
            var result = modelInput[0].feels["sad"] > modelInput[0].feels["happy"] ? 0 : 1;
            
            var mlContext = new MLContext();
            TweetML.ModelInput[] newData = new[]
            {
                new TweetML.ModelInput()
                {
                    Text = modelInput[0].Text,
                    Label = result
                }
            };
            var data = mlContext.Data.LoadFromEnumerable<TweetML.ModelInput>(newData);
            var retrainedModel = TweetML.RetrainPipeline(mlContext, data);
            mlContext.Model.Save(retrainedModel, data.Schema, "..\\Domain\\TweetML2.zip");
            var res = _context.GetCollection<TweetDto>("RequestedTweets").DeleteOne(Builders<TweetDto>.Filter.Eq("_id", objectId));
            Console.WriteLine(res.DeletedCount);
        }
    }
}