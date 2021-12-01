using System.Threading.Tasks;
using Application.Persistence;
using Tweet = Domain.Entities.Tweet;

namespace Persistence.TwitterExternalAPI
{
    public class ExternalTwitterRepository : IExternalTweetRepository
    {
        private readonly ITwitterHelper _twitterHelper;
        
        public ExternalTwitterRepository(ITwitterHelper twitterHelper)
        {
            _twitterHelper = twitterHelper;
           
        }
        
        public async Task<Tweet> GetTweetById(string id)
        {
            var tweetResponse = await _twitterHelper._twitterClient.TweetsV2.GetTweetAsync(id);
            var tweetV2 = tweetResponse.Tweet;
            Tweet tweet = new Tweet();
            tweet.Text = tweetV2.Text;
            tweet.User = tweetV2.ContextAnnotations[0].Entity.Name;
            tweet.Date = tweetV2.CreatedAt.ToString();
            return tweet;
        }
    }
}