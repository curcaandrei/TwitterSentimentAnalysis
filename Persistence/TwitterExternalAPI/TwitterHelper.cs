using Microsoft.Extensions.Options;
using Tweetinvi;

namespace Persistence.TwitterExternalAPI
{
    public class TwitterHelper : ITwitterHelper
    {
        public string apiKey { get; set; }
        public string apiSecret { get; set; }
        public string accessToken { get; set; }
        public string accessSecret { get; set; }
        public TwitterClient _twitterClient { get; }

        public TwitterHelper(IOptions<TwitterSettings> configuration)
        {
            apiKey = configuration.Value.apiKey;
            apiSecret = configuration.Value.apiSecret;
            accessToken = configuration.Value.accessToken;
            accessSecret = configuration.Value.accessSecret;
            _twitterClient = new TwitterClient(apiKey, apiSecret, accessToken, accessSecret);
        }


    }
}