using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v1
{
    public class TweetRepository : Repository<Tweet>, ITwitterRepository
    {
        public TweetRepository(TweetContext context) : base(context)
        {
        }
    }
}