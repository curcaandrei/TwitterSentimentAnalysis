using MongoDB.Driver;

namespace Persistence.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}