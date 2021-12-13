using System.Threading.Tasks;
using Tweet = Domain.Entities.Tweet;

namespace Application.Persistence
{
    public interface IExternalTweetRepository
    {
        public Task<Tweet> GetTweetById(string id, bool unitTest = false);

        public Task<string> PostToGetAuth();

        public Task<string> ValidateAuth(string queryValue);
    }
}