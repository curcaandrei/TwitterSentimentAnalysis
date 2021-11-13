using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Persistence;
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

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}