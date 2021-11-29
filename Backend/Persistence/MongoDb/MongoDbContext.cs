using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Persistence.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase Database { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle SessionHandle { get; set; }
        
        public MongoDbContext(IOptions<MongoSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            Database = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }   
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
    }
}