using System.Collections.Generic;
using Application.Interfaces;
using Domain.Common;
using MongoDB.Driver;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class MongoDbRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MongoDbContext _mongoDbContext;

        private IMongoCollection<T> Collection { get; set; }
        
        public MongoDbRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            Collection = _mongoDbContext.DbSet<T>();
        }

        
        public void Create(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public T SelectById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> SelectAll()
        {
            throw new System.NotImplementedException();
        }
    }
}