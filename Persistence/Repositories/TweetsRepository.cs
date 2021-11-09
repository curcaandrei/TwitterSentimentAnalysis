using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class TweetsRepository : RavenDbRepository<Tweet>, ITweetsRepository
    {
        public TweetsRepository(IRavenDbContext context) : base(context)
        {
        }


        public Tweet SelectByText(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}