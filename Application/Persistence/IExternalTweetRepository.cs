using System.Threading.Tasks;
using Tweet = Domain.Entities.Tweet;

namespace Application.Persistence
{
    public interface IExternalTweetRepository
    {
        public Task<Tweet> GetTweetById(string id);
    }
}