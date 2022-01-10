using System;
using System.Collections.Generic;
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

        public async Task<List<TweetDto>> ListAllAsync()
        {
            List<Tweet> tweets =  _context.GetCollection<Tweet>("RequestedTweets").Find(_ => true).ToList();
            List<TweetDto> tweetDtos = new List<TweetDto>();
            
            foreach (var variable in tweets)
            {
                Tweet? t = variable as Tweet;
                TweetDto tweetDto = new TweetDto();
                if (t != null)
                {
                    tweetDto.Feels = t.feels;
                    tweetDto.Date = t.Date;
                    tweetDto.Id = t.Id.ToString();
                    tweetDto.Text = t.Text;
                    tweetDto.User = t.User;
                    tweetDto.Username = t.Username;
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