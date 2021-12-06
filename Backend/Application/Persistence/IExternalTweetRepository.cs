using System.Threading.Tasks;
using Tweetinvi.Models.V2;
using Tweet = Domain.Entities.Tweet;

namespace Application.Persistence
{
    public interface IExternalTweetRepository
    {
        public Task<Tweet> GetTweetById(string id);

        public Task<string> PostToGetAuth();

        public Task<string> ValidateAuth(string queryValue);
    }
}