using Application.Persistence;
using Domain.Entities;
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
    }
}