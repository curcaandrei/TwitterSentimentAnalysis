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
            apiKey = configuration.Value.ApiKey;
            apiSecret = configuration.Value.ApiSecret;
            accessToken = configuration.Value.AccessToken;
            accessSecret = configuration.Value.AccessSecret;
            _twitterClient = new TwitterClient(apiKey, apiSecret, accessToken, accessSecret);
        }


    }
}