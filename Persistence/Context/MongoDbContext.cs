using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Persistence.Context
{
    public class MongoDbContext : DbContext, IApplicationDbContext
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;

        public MongoDbContext(MongoClient mongoClient, IMongoDatabase database)
        {
            _mongoClient = mongoClient;
            _database = database;
        }

        public IMongoCollection<T> DbSet<T>() where T : BaseEntity
        {
            var table = typeof(T).GetCustomAttribute<TableAttribute>(false).Name;
            return _database.GetCollection<T>(table);
        }

        public DbSet<Tweet> Tweets { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}