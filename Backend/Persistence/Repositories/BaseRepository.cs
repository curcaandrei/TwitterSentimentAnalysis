using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IMongoDbContext _context;
        public BaseRepository(IMongoDbContext dbContext)
        {
            _context = dbContext;
        }
        
        public async Task<List<TweetDto>> ListAllAsync(int pageNr)
        {
            List<T> tweets =  _context.GetCollection<T>(typeof(T).Name).Find(_ => true).Skip((pageNr - 1) * 10).Limit(10).ToList();
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

        public async Task<T> AddAsync(T entity)
        {
            await _context.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
            return entity;
        }
        
        public UpdateResult UpdateAsync(string id, Dictionary<string, float>  feels)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).UpdateOne(Builders<T>.Filter.Eq("_id", objectId),
                Builders<T>.Update.Set("feels", feels));
        }

        public DeleteResult DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).DeleteOne(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}