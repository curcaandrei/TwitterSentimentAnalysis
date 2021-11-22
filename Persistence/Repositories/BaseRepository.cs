using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.DeleteTweet;
using Application.Persistence;
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
        
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.GetCollection<T>(typeof(T).Name).Find(_ => true).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
            return entity;
        }

        public UpdateResult UpdateAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateAsync(string id, Dictionary<string, float>  entity)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).UpdateOne(Builders<T>.Filter.Eq("_id", objectId),
                Builders<T>.Update.Set("feels", entity));
        }

        public DeleteResult DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            return _context.GetCollection<T>(typeof(T).Name).DeleteOne(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}