using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.Repositories
{
    public class TweetRepository : BaseRepository<Tweet>, ITweetsRepository
    {
        private IMongoCollection<Tweet> _collection;
        
        public TweetRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            _collection = dbContext.GetCollection<Tweet>(typeof(Tweet).Name);
        }
        
        public Task<Tweet> GetByIdAsync(ObjectId id)
        {
            return _collection.FindAsync(x => x.Id == id).Result.FirstAsync();
        }
    }
}