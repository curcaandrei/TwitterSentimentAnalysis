using Tweetinvi;

namespace Persistence.TwitterExternalAPI
{
    public interface ITwitterHelper
    {
        public string apiKey { get; set; }
        public string apiSecret { get; set; }
        public string accessToken { get; set; }
        public string accessSecret { get; set; }
        public TwitterClient _twitterClient { get; }
    }
}